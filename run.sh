#!/bin/bash
#In the official documentation the line above always has to be the first line of any script file.

# Author: Anthony Sam
# Email: anthonysam538@csu.fullerton.edu
# Course: CPSC 223N
# Semester: Fall 2019
# Assignment #2
# Program Name: Traffic Signal

#This is a bash shell script to be used for compiling, linking, and executing the C sharp files of this assignment.
#Execute this file by navigating the terminal window to the folder where this file resides, and then enter either of the commands below:
#  sh run.sh OR ./build.sh

echo "First, remove any potentially outdated .dll or .exe files using the keyword rm"
rm *.dll
rm *.exe

echo "Display the list of the remaining source files in the terminal using the keyword ls"
ls -l

echo "Compile TrafficSignalForm.cs to create the file: TrafficSignalForm.dll"
mcs -target:library -r:System.Windows.Forms.dll -r:System.Drawing.dll -out:TrafficSignalForm.dll TrafficSignalForm.cs

echo "Compile TrafficSignalMain.cs and link the previously created dll file(s) to create an executable (.exe) file."
mcs -r:System.Windows.Forms.dll -r:TrafficSignalForm.dll -out:TrafficSignal.exe TrafficSignalMain.cs

echo "Display the updated list of files in the folder, now including the newly created .dll and .exe files"
ls -l

echo "Run the Assignment 1 program."
./TrafficSignal.exe

echo "The script has terminated."
