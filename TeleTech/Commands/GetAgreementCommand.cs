using System.IO;
using TeleTech.Model;
using TeleTech.Stores;
using Word = Microsoft.Office.Interop.Word;


namespace TeleTech.Commands
{
    public class GetAgreementCommand : CommandBase

    {
        //сделать относительный путь
        private string _fileName = "C:\\Users\\njab\\source\\repos\\TeleTech\\TeleTech\\Договор-оказания-услуг.doc";

        private ArmContext _armContext = new ArmContext();
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
            var sims = _armContext.Simissuances.Where(x=> x.PassportNumber == _user.PassportId)?.FirstOrDefault();
            var items = new Dictionary<string, string>()
                    {

                        { "CITY",  _user.Address },
                        { "DAY", DateTime.Today.Day.ToString() },
                        { "MONTH", DateTime.Today.Month.ToString() },
                        { "YEAR", DateTime.Today.Year.ToString() },
                        { "<EMPLOYEENAME>",GetLastName(_user.PassportId) == null ? "" : GetLastName(_user.PassportId)},
                        { "<TARIFF>", _armContext.Sims.Where(x=>x.SimcardNumber == sims.SimcardNumber).FirstOrDefault().TariffName},
                        { "<USERNAME>",  _user.Name },
                        { "<SURNAME>",  _user.Surname },
                        { "<PATRONOMIC>",  _user.Patronymic },
                        { "<PASSPORTID>",  _user.PassportId.ToString() },
                        { "<BIRTH>",  _user.Birthday.ToString() },
                        { "<EXPIRYDATE>",  sims.ExpiryDate.ToString() },
                        { "<FULLDATE>",  sims.IssueDate.ToString() },



                    };
            Process(items);

            
        }
        public string GetLastName(int passportNumber)
        {
            var a = _armContext.Simissuances
.Where(x => x.PassportNumber == passportNumber)
.FirstOrDefault();
            if (a == null)
                return "";
            var b = _armContext.Employees.Where(x => x.UserId == a.EmployeeId).FirstOrDefault()?.LastName;
            return a == null ? "" : b;
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
        public GetAgreementCommand()
        {
            
        }
    }
}
