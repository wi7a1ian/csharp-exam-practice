using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnblockingTheUI
{
    ///
    /// The implementation of the BackgroundWork class ensures that in a Windows Forms
    /// or WPF application the RunWorkerCompleted event handler is run by a UI thread
    /// if the RunWorkerAsync is called by the UI thread. In other words, if you start the
    /// background work inside an event handler, the completion event will be run in
    /// the UI thread. You can see the implications of this in the next two sections.
    /// 

    // Multithreaded Windows Forms Applications
    /*
    void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
        if (this.InvokeRequired) {
            this.Invoke(
                new Action<string>(UpdateLabel),
                e.Result.ToString());
        }
        else {
            UpdateLabel(e.Result.ToString());
        }
    }
    private void UpdateLabel(string text) {
        lblResult.Text = text;
    }
    */




    // Multithreaded WPF Applications
    /*
    private BackgroundWorker worker;
    public MainWindow() {
        InitializeComponent();
        worker = new BackgroundWorker();
        worker.DoWork += worker_DoWork;
        worker.RunWorkerCompleted += worker_RunWorkerCompleted;
    }
    void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
        this.Dispatcher.Invoke(()=> lblResult.Content = e.Result);
    }
    void worker_DoWork(object sender, DoWorkEventArgs e) {
        e.Result = DoIntensiveCalculations();
    }
    private void btnRun_Click(object sender, EventArgs e) {
        if (!worker.IsBusy) {
            worker.RunWorkerAsync();
        }
    }
    static double DoIntensiveCalculations() {}
    */




    // WORKING WITH THE TASK PARALLEL LIBRARY
    // If you were to use tasks in the Windows Forms application and you want to call the UpdateLabel method on the UI thread, you would use the following:
    /*
    Task.Factory.StartNew(UpdateLabel,
        CancellationToken.None,
        TaskCreationOptions.None,
        TaskScheduler.FromCurrentSynchronizationContext());
    */
    //By creating the task this way, it will be executed by the UI thread as soon as the UI thread can process it.
}
