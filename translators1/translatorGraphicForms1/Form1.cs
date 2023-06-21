using System.Linq.Expressions;
using translators1;
namespace translatorGraphicForms1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text = textBoxInputCode.Text;
            translators1.LexAnalyzer.LexicalAnalyzer analyzer = new(text);

            translators1.LexAnalyzer.lexicalController lexicalController = new(analyzer.Analyze());
            textBoxLiterals.Text = lexicalController.toStringWithFormating();



            try
            {
                translators1.SyntaxAnalyzer.Parser interpreter = new(text);
                translators1.SyntaxAnalyzer.Interfaces.Expression node = interpreter.Parse();

                node.Accept(new translators1.SyntaxAnalyzer.ValueCalculator());
                translators1.LLGen.LLConverter converter = new(@"C:\Users\Kirill\Desktop\result.txt");
                textBoxOutputCode.Text += converter.Convert();
            }
            catch (translators1.SyntaxAnalyzer.InvalidSyntaxException ex)
            {
                textBoxOutputCode.Text += ex.Message;
            }
            catch (Exception ex)
            {
                textBoxOutputCode.Text += ex;
            }




        }

        private void buttonClean_Click(object sender, EventArgs e)
        {
            textBoxInputCode.Text = "";
            textBoxLiterals.Text = "";
            textBoxOutputCode.Text = "";
            textBoxInputCode.Refresh();
            textBoxOutputCode.Refresh();
            textBoxLiterals.Refresh();
        }
    }
}