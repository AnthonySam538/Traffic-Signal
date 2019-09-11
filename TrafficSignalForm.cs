// Author: Anthony Sam
// Email: anthonysam538@csu.fullerton.edu
// Course: CPSC 223N
// Semester: Fall 2019
// Assignment #2
// Program Name: Traffic Signal
//
// Name of this file: TrafficSignalMain.cs
// Purpose of this file: Launch the form where the traffic signal will be displayed.
// Purpose of this entire program: Display a traffic signal. This program contains exactly one clock.
// Source files in this program: TrafficSignalForm.cs, TrafficSignalMain.cs

using System;
using System.Windows.Forms;
using System.Drawing;
using System.Timers;

public class TrafficSignalForm : Form
{
  // Set the size of the window (Also used with positioning various elements)
  private const int formHeight = 900;
  private const int formWidth = formHeight * 9/16; //creates a 9:16 aspect ratio

  // Create the needed Rectangle and SolidBrush objects
  private static Rectangle topLight;
  private static Rectangle middleLight;
  private static Rectangle bottomLight;
  private static SolidBrush topBrush = new SolidBrush(Color.DarkGray);
  private static SolidBrush middleBrush = new SolidBrush(Color.DarkGray);
  private static SolidBrush bottomBrush = new SolidBrush(Color.DarkGray);

  // Create Controls (listed from top to bottom, left to right)
  private Label title = new Label();
  private Button startButton = new Button();
  private GroupBox radioButtonBox = new GroupBox();
  private RadioButton slowButton = new RadioButton();
  private RadioButton fastButton = new RadioButton();
  private Button pauseButton = new Button();
  private Button exitButton = new Button();

  // Create Timer
  private static System.Timers.Timer myTimer = new System.Timers.Timer();

  // Miscellaneous stuff
  private static Size myButtonSize = new Size(85, 25);
  private static int distanceBetweenButtons = formWidth/5-myButtonSize.Width;

  public TrafficSignalForm()
  {
    // Set up the form/window
    Text = "Traffic Signal";
    Size = new Size(formWidth,formHeight);
    BackColor = Color.Black;

    // Set up the Rectangles
    topLight.Size = new Size(formHeight/5, formHeight/5);
    topLight.Location = new Point(formWidth/2 - topLight.Width/2, formHeight*3/20);
    middleLight.Size = topLight.Size;
    middleLight.Location = new Point(topLight.Left, topLight.Bottom + formHeight/20);
    bottomLight.Size = topLight.Size;
    bottomLight.Location = new Point(topLight.Left, middleLight.Bottom + formHeight/20);

    // Set up the title label
    title.Text = "Traffic Signal by Anthony Sam";
    title.Size = new Size(formWidth, formHeight/10);
    title.Location = new Point(0,0);
    title.BackColor = Color.LawnGreen;
    title.TextAlign = ContentAlignment.MiddleCenter;

    // Set up the start button
    startButton.Text = "Start";
    startButton.Size = myButtonSize;
    startButton.Location = new Point(distanceBetweenButtons, formHeight*19/20 - myButtonSize.Height/2);
    startButton.BackColor = Color.Magenta;

    // Set up the GroupBox and its RadioButtons
    radioButtonBox.Text = "Rate of Change";
    radioButtonBox.Size = new Size(myButtonSize.Width*2, myButtonSize.Height*2);
    radioButtonBox.Location = new Point(startButton.Right+distanceBetweenButtons, formHeight*19/20 - radioButtonBox.Height/2);
    radioButtonBox.BackColor = Color.DarkOrchid;

    slowButton.Text = "Slow";
    slowButton.Size = new Size(radioButtonBox.Width/2, myButtonSize.Height);
    slowButton.Location = new Point(0, myButtonSize.Height);

    fastButton.Text = "Fast";
    fastButton.Size = slowButton.Size;
    fastButton.Location = new Point(myButtonSize.Width, myButtonSize.Height);

    // Set up the pause button
    pauseButton.Text = "Pause";
    pauseButton.Size = myButtonSize;
    pauseButton.Location = new Point(radioButtonBox.Right+distanceBetweenButtons, startButton.Top);
    pauseButton.BackColor = startButton.BackColor;

    // Set up the exit button
    exitButton.Text = "Exit";
    exitButton.Size = myButtonSize;
    exitButton.Location = new Point(pauseButton.Right+distanceBetweenButtons, startButton.Top);
    exitButton.BackColor = startButton.BackColor;

    // Add the RadioButtons to the GroupBox
    radioButtonBox.Controls.Add(slowButton);
    radioButtonBox.Controls.Add(fastButton);

    // Add the controls to the form
    Controls.Add(title);
    Controls.Add(startButton);
    Controls.Add(pauseButton);
    Controls.Add(exitButton);
    Controls.Add(radioButtonBox);

    // Tell the events which method to call
    myTimer.Elapsed += new ElapsedEventHandler(toggleLight);
    startButton.Click += new EventHandler(start);
    slowButton.CheckedChanged += new EventHandler(slowDown);
    fastButton.CheckedChanged += new EventHandler(speedUp);
    pauseButton.Click += new EventHandler(pause);
    exitButton.Click += new EventHandler(exit);
  }

  // This method illustrates the screen
  protected override void OnPaint(PaintEventArgs e)
  {
    Graphics graphics = e.Graphics;

    graphics.FillEllipse(topBrush, topLight);
    graphics.FillEllipse(middleBrush, middleLight);
    graphics.FillEllipse(bottomBrush, bottomLight);

    graphics.FillRectangle(Brushes.Cyan, 0, formHeight - formHeight/10, formWidth, formHeight/10);

    // This calls the superclass's OnPaint()
    base.OnPaint(e);
  }

  protected void toggleLight(Object sender, ElapsedEventArgs e)
  {
    if(topBrush.Color == Color.Red) //light is red (change to green)
    {
      topBrush.Color = Color.DarkGray;
      bottomBrush.Color = Color.Green;
      myTimer.Interval *= 0.75; //8s --> 6s and 4s --> 3s
    }
    else if(middleBrush.Color == Color.Yellow) //light is yellow (change to red)
    {
      topBrush.Color = Color.Red;
      middleBrush.Color = Color.DarkGray;
      myTimer.Interval *= 4; //2s --> 8s and 1s --> 4s
    }
    else //light is green (change to yellow)
    {
      middleBrush.Color = Color.Yellow;
      bottomBrush.Color = Color.DarkGray;
      myTimer.Interval /= 3; //6s --> 2s and 3s --> 1s
    }
    Invalidate();
    System.Console.WriteLine("The timer has toggled the light.");
  }

  protected void start(Object sender, EventArgs e)
  {
    myTimer.Interval = 4000; //Note that this will be doubled when slowButton.Checked = true;
    myTimer.Enabled = true;
    slowButton.Checked = true;
    topBrush.Color = Color.Red;
    Invalidate();
    System.Console.WriteLine("You clicked on the Start button.");
  }

  protected void exit(Object sender, EventArgs e)
  {
    System.Console.WriteLine("You clicked on the Exit button. This program will now end.");
    Close();
  }

  protected void slowDown(Object sender, EventArgs e)
  {
    // Only slow down if the slowButton is currently checked
    if(slowButton.Checked)
    {
      myTimer.Interval *= 2;
      System.Console.WriteLine("The Slow button has been checked. Slowing down...");
    }
  }

  protected void speedUp(Object sender, EventArgs e)
  {
    // Only speed up if the fastButton is currently checked
    if(fastButton.Checked)
    {
      myTimer.Interval /= 2;
      System.Console.WriteLine("The Fast button has been checked. Speeding up...");
    }
  }

  protected void pause(Object sender, EventArgs e)
  {
    if(myTimer.Enabled)
    {
      myTimer.Stop();
      pauseButton.Text = "Resume";
      System.Console.WriteLine("You clicked on the Pause button.");
    }
    else
    {
      myTimer.Start();
      pauseButton.Text = "Pause";
      System.Console.WriteLine("You clicked on the Resume button.");
    }
  }
}
