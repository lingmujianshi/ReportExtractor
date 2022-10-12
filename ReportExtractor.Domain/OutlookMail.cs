using Microsoft.Office.Interop.Outlook;
using System.Security.Cryptography.X509Certificates;

namespace ReportExtractor.Domain
{
    public class OutlookMail
    {
        public string To { get; set; }
        public string CC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public void CreateMail()
        {
            var outlook = new Application();
            MailItem mailItem = outlook.CreateItem(OlItemType.olMailItem);
            if (mailItem != null)
            {
                // To
                if (To != "")
                {
                    Recipient to = mailItem.Recipients.Add(To);
                    to.Type = (int)OlMailRecipientType.olTo;
                }

                // Cc
                if (CC != "")
                {
                    Recipient cc = mailItem.Recipients.Add(CC);
                    cc.Type = (int)OlMailRecipientType.olCC;
                }

                // アドレス帳の表示名で表示できる
                mailItem.Recipients.ResolveAll();
                // 件名
                mailItem.Subject = Subject;
                // 本文
                mailItem.HTMLBody = Body;
                // 表示(Displayメソッド引数のtrue/falseでモーダル/モードレスウィンドウを指定して表示できる)
                mailItem.Display(false);

            }
        }
    }
}
