using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsPowerToolsDemo;

public partial class EmptyForm : Form
{
    public EmptyForm()
    {
        InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        TaskCompletionSource<bool> tcs = new();

        var task = Task.Run(async () =>
        {
            await Task.Delay(500).ConfigureAwait(false);

            await Task.Run(() =>
            {
                this.BeginInvoke(() => button1.Text = "Hello World!");
            }).ConfigureAwait(false);

            var asyncResult = this.BeginInvoke(() =>
            {
                try
                {
                    tcs.SetResult(true);
                    label1.Text = "Hello World!";
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });

            return asyncResult;
        });

        var result = task.GetAwaiter().GetResult();
        this.EndInvoke(result);

        var result2 = tcs.Task.Result;
    }
}
