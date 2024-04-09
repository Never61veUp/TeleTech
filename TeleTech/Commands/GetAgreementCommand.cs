using System.IO;
using TeleTech.Model;
using TeleTech.Stores;
using Word = Microsoft.Office.Interop.Word;


namespace TeleTech.Commands
{
    internal class GetAgreementCommand : CommandBase

    {
        //сделать относительный путь
        private string _fileName = "C:\\Users\\njab\\source\\repos\\TeleTech\\TeleTech\\Договор-оказания-услуг.doc";


        private AccountStore _accountStore;
        private FileInfo _fileInfo;
        private User _user;

        public GetAgreementCommand(User user)
        {
            _user = user;

            if (File.Exists(_fileName))
            {
                _fileInfo = new FileInfo(_fileName);
            }
            else
            {
                throw new ArgumentException("FileNotFound");
            }
        }

        public override void Execute(object? parameter)

        {

            var items = new Dictionary<string, string>()
                    {

                        { "CITY",  _user.Address },
                        { "DAY", DateTime.Today.Day.ToString() },
                        { "MONTH", DateTime.Today.Month.ToString() },
                        { "YEAR", DateTime.Today.Year.ToString() }
                        //{ "EMPLOYEENAME", _accountStore.CurrentAccount.LastName }
                        //{ "TARIFF", _user.T }

                    };
            Process(items);
        }







        private bool Process(Dictionary<string, string> items)
        {
            Word.Application app = null;
            try
            {
                app = new Word.Application();
                Object file = _fileInfo.FullName;

                Object missing = Type.Missing;

                app.Documents.Open(_fileName);

                foreach (var item in items)
                {
                    Word.Find find = app.Selection.Find;
                    find.Text = item.Key;
                    find.Replacement.Text = item.Value;

                    Object wrap = Word.WdFindWrap.wdFindContinue;
                    Object replace = Word.WdReplace.wdReplaceAll;

                    find.Execute(FindText: Type.Missing,
                        MatchCase: false,
                        MatchWholeWord: false,
                        MatchWildcards: false,
                        MatchSoundsLike: missing,
                        MatchAllWordForms: false,
                        Forward: true,
                        Wrap: wrap,
                        Format: false,
                        ReplaceWith: missing, Replace: replace);
                }
                Object newFileName = Path.Combine(_fileInfo.DirectoryName, DateTime.Now.ToString("yyyyMMdd HHmmss") + _fileInfo.Name);
                app.ActiveDocument.SaveAs2(newFileName);
                app.ActiveDocument.Close();
                return true;
            }
            catch
            {
                throw new Exception();
            }
            finally
            {
                if (app != null)
                    app.Quit();
            }
            return false;
        }
    }
}
