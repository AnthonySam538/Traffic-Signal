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
  private const int formWidth = formHeight * 16/9; //creates a 16:9 aspect ratio

  // Create the needed Rectangles and SolidBrush
  private static Rectangle trafficSignal; //the back of the trafficSignal

  // Create Controls
  private Label title = new Label();
  private Button startButton = new Button();
  private Button exitButton = new Button();
  private RadioButton slowButton = new RadioButton();
  private RadioButton fastButton = new RadioButton();
  private GroupBox radioButtonBox = new GroupBox();

  // Create Timer
  private static Timer myTimer = new Timer();

  // This is the standard button size
  private static Size myButtonSize = new Size(85, 25);

  public TrafficSignalForm()
  {
    // Set up the form/window
    Text = "Traffic Signal";
    Size = new Size(formWidth,formHeight);
    BackColor = Color.DarkOrchid;

    // Set up the Rectangle
    trafficSignal.Size = new Size(formWidth, formHeight/10);

    // Set up the title label
    title.Text = "Traffic Signal by Anthony Sam";
    title.Size = yellowRectangle.Size;
    title.Location = new Point(0,0);
    title.BackColor = Color.LawnGreen;
    title.TextAlign = ContentAlignment.MiddleCenter;

    // Set up the start button
    startButton.Text = "Start";
    startButton.Size = myButtonSize;
    // startButton.Location = new Point();
    startButton.BackColor = Color.Magenta;

    // Set up the exit button
    exitButton.Text = "Exit";
    exitButton.Size = myButtonSize;
    // exitButton.Location = new Point();
    exitButton.BackColor = Color.Magenta;

    // Set up the RadioButtons and their GroupBox
    slowButton.Text = "Slow";
    fastButton.Text = "Fast";

    // Add the RadioButtons to the GroupBox
    radioButtonBox.Controls.Add(slowButton);
    radioButtonBox.Controls.Add(fastButton);

    // Add the controls to the form
    Controls.Add(title);
    Controls.Add(startButton);
    Controls.Add(exitButton);
    Controls.Add(GroupBox);

    // Tell the events which method to call
    myTimer.Elapsed += new ElapsedEventHandler(toggleLight);
    startButton.Click += new EventHandler(start);
    exitButton.Click += new EventHandler(exitProgram);
    slowButton.CheckedChanged += new EventHandler(slowDown);
    fastButton.CheckedChanged += new EventHandler(speedUp);
  }

  // This method illustrates the screen
  protected override void OnPaint(PaintEventArgs e)
  {
    Graphics graphics = e.Graphics;

    // This calls the superclass's OnPaint()
    base.OnPaint(e);
  }

  protected void start(Object sender, EventArgs e)
  {
    myTimer.Interval = ;
    myTimer.Enabled = true;
  }

  protected void exitProgram(Object sender, EventArgs e)
  {
    System.Console.WriteLine("You clicked on the Exit button. This program will now end.");
    Close();
  }

  protected void slowDown(Object sender, EventArgs e)
  {}

  protected void speedUp(Object sender, EventArgs e)
  {}
}
