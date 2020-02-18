using System;
using System.Security.Cryptography;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Xml;

namespace wsConsultaFepe
{
    public class PasswordDigestMessageInspector : IClientMessageInspector
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public PasswordDigestMessageInspector(string username, string password)
        {
            Username = username;
            Password = password;
        }

        #region IClientMessageInspector Members

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            return;
        }

        public object BeforeSendRequest(ref Message request, System.ServiceModel.IClientChannel channel)
        {
            DateTime created = DateTime.Now;
            string createdString = created.ToString("yyyy-MM-ddTHH:mm:ss.fff-05:00");

            var envelope = new XmlDocument();

            XmlNode securityNode = envelope.CreateNode(XmlNodeType.Element, "wsse:Security", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");

            XmlNode usernameTokenNode = envelope.CreateNode(XmlNodeType.Element, "wsse:UsernameToken", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
            XmlElement userElement = usernameTokenNode as XmlElement;
            userElement.SetAttribute("xmlns:wsu", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");

            XmlNode userNameNode = envelope.CreateNode(XmlNodeType.Element, "wsse:Username", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
            userNameNode.InnerXml = Username;

            XmlNode pwdNode = envelope.CreateNode(XmlNodeType.Element, "wsse:Password", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
            XmlElement pwdElement = pwdNode as XmlElement;
            pwdElement.SetAttribute("Type", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText");
            pwdNode.InnerXml = SHA256Hash(Password).ToLower();

            XmlNode nonceNode = envelope.CreateNode(XmlNodeType.Element, "wsse:Nonce", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd");
            XmlElement nonceElement = nonceNode as XmlElement;
            nonceElement.SetAttribute("EncodingType", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary");
            nonceNode.InnerXml = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(DateTime.Now.ToBinary().ToString()));

            XmlNode createNode = envelope.CreateNode(XmlNodeType.Element, "wsu", "Created", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd");
            XmlElement createElement = createNode as XmlElement;
            createNode.InnerXml = createdString;

            usernameTokenNode.AppendChild(userNameNode);
            usernameTokenNode.AppendChild(pwdNode);
            usernameTokenNode.AppendChild(nonceNode);
            usernameTokenNode.AppendChild(createNode);

            //securityNode.AppendChild(usernameTokenNode);

            var securityHeader = MessageHeader.CreateHeader("Security", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd", usernameTokenNode);

            request.Headers.Add(securityHeader);

            return Convert.DBNull;
        }

        #endregion

        private static byte[] buildBytes(string nonce, string createdString, string basedPassword)
        {
            byte[] nonceBytes = System.Convert.FromBase64String(nonce);
            byte[] time = Encoding.UTF8.GetBytes(createdString);
            byte[] pwd = Encoding.UTF8.GetBytes(basedPassword);

            byte[] operand = new byte[nonceBytes.Length + time.Length + pwd.Length];
            Array.Copy(nonceBytes, operand, nonceBytes.Length);
            Array.Copy(time, 0, operand, nonceBytes.Length, time.Length);
            Array.Copy(pwd, 0, operand, nonceBytes.Length + time.Length, pwd.Length);

            return operand;
        }

        public static byte[] SHAOneHash(byte[] data)
        {
            using (SHA256 sha1 = new SHA256Managed())
            {
                var hash = sha1.ComputeHash(data);
                string da = BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes("FepePri20$"))).Replace("-", "");
                return hash;
            }
        }

        public static string SHA256Hash(string pass)
        {
            using (SHA256 sha1 = new SHA256Managed())
            {
                string da = BitConverter.ToString(sha1.ComputeHash(Encoding.UTF8.GetBytes(pass))).Replace("-", "");
                return da;
            }
        }
    }
}