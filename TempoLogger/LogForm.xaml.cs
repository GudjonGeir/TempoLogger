﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TempoLogger.Models;

namespace TempoLogger
{
	/// <summary>
	/// Interaction logic for LogForm.xaml
	/// </summary>
	public partial class LogForm : Window
	{
		public WorkLog Model { get; set; }
		public LogForm()
		{
			InitializeComponent();

			// Create new Log (else is edit)
			if (Model == null)
			{
				Model = new WorkLog
				{
					Date = DateTime.Now.Date,
				};
			}

			TxtIssue.Text = Model.Issue;
			DpDate.SelectedDate = Model.Date;
			TxtStart.Text = Model.Start;
			TxtEnd.Text = Model.End;
			TxtDuration.Text = Model.Duration;
			TxtComment.Text = Model.Comment;
		}

		private void BtnSave_Click(object sender, RoutedEventArgs e)
		{
			if (!Validate()) return;

			Model.Issue = TxtIssue.Text;
			Model.Date = DpDate.SelectedDate.Value;
			Model.Start = TxtStart.Text;
			Model.End = TxtEnd.Text;
			Model.Duration = TxtDuration.Text;
			Model.Comment = TxtComment.Text;

			DialogResult = true;
			Close();
		}

		private bool Validate()
		{
			var issueRegex = @"^[[A-Za-z]+\-[0-9]+$";
			var timeRegex = @"^[0-9][0-9]?:[0-9]{2}$";
			//var durationRegex = @"^(([0-9]+)h)?\s*(([0-9]+)m)?$";

			if (!Regex.Match(TxtIssue.Text, issueRegex).Success)
			{
				LblError.Content = "Issue is not in correct format";
				return false;
			}

			if (!DpDate.SelectedDate.HasValue)
			{
				LblError.Content = "You need to pick a date";
				return false;
			}

			if (!Regex.Match(TxtStart.Text, timeRegex).Success)
			{
				LblError.Content = "Start is not in correct format";
				return false;
			}

			if (!Regex.Match(TxtEnd.Text, timeRegex).Success)
			{
				LblError.Content = "End is not in correct format";
				return false;
			}

			//if (string.IsNullOrEmpty(TxtDuration.Text) ||!Regex.Match(TxtDuration.Text, durationRegex).Success)
			//{
			//	LblError.Content = "Duration is not in correct format";
			//	return false;
			//}

			if (string.IsNullOrEmpty(TxtComment.Text))
			{
				LblError.Content = "You need to write a comment";
				return false;
			}

			LblError.Content = "";
			return true;
		}
	}
}
