﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
namespace Angles {
	class OptionsReg {
		private const string keyPath0 = "HKEY_CURRENT_USER\\SOFTWARE\\VVV\\Angles\\Options";
		private string keyPath = "";
		private List<Control> lst = new List<Control>();
		public OptionsReg(Window w) {
			keyPath = keyPath0 + "\\" + w.Name;
		} // ////////////////////////////////////////////////////////////////////////////////
		public OptionsReg(Window w, Control[] ctrls) {
			keyPath = keyPath0 + "\\" + w.Name;
			Init(ctrls);
		} // ////////////////////////////////////////////////////////////////////////////////
		public void SaveAll() {
			foreach(Control ctrl in lst) {
				string ctrlVal, keyName = ctrl.Name;
				Type type = ctrl.GetType();
				switch(type.Name) {
				case "TextBox":
					ctrlVal = ((TextBox)ctrl).Text;
					Registry.SetValue(keyPath, keyName, ctrlVal);
					break;
				case "CheckBox":
					ctrlVal = ((CheckBox)ctrl).IsChecked != null ? ((CheckBox)ctrl).IsChecked.ToString() : "Null";
					Registry.SetValue(keyPath, keyName, ctrlVal);
					break;
				case "ComboBox":
					int idx = ((ComboBox)ctrl).SelectedIndex;
					Registry.SetValue(keyPath, keyName + "_SELECT", idx.ToString());
					break;
				default:
					throw new ArgumentOutOfRangeException("Invalid case: " + type.Name);
				}
			}
		} // /////////////////////////////////////////////////////////////////////////////////
		public void Init(Control ctrl) {
			Type type = ctrl.GetType();
			switch(type.Name) {
			case "TextBox":
				InitTextBox((TextBox)ctrl);
				break;
			case "CheckBox":
				InitCheckBox((CheckBox)ctrl);
				break;
			case "ComboBox":
				InitComboBox((ComboBox)ctrl);
				break;
			default:
				throw new ArgumentOutOfRangeException("Invalid case: " + type.Name);
			}
		} // ///////////////////////////////////////////////////////////////////////////////////
		public void Init(Control[] ctrls) {
			foreach(Control ctrl in ctrls)
				Init(ctrl);
		} // ///////////////////////////////////////////////////////////////////////////////////
		public void InitTextBox(TextBox ctrl) {
			string keyName = ctrl.Name;
			lst.Add(ctrl);
			object ret = Registry.GetValue(keyPath, keyName, null);
			if(ret == null) {
				Registry.SetValue(keyPath, keyName, ctrl.Text);
			} else {
				string readRegVal = ret.ToString();
				if(readRegVal.Length == 0)
					Registry.SetValue(keyPath, keyName, ctrl.Text);
				else
					ctrl.Text = readRegVal;
			}
		} // ///////////////////////////////////////////////////////////////////////////////////
		public void InitComboBox(ComboBox ctrl) {
			string keyName = ctrl.Name;
			lst.Add(ctrl);
			object retcnt = Registry.GetValue(keyPath, keyName + "_SELECT", null);
			if(retcnt != null) {
				int idx = Convert.ToInt32(retcnt);
				if(ctrl.Items.Count > 0)
					ctrl.SelectedIndex = idx;
			}
		} // ///////////////////////////////////////////////////////////////////////////////////
		public void InitCheckBox(CheckBox ctrl) {
			string keyName = ctrl.Name, ctrlVal;
			ctrlVal = ((CheckBox)ctrl).IsChecked != null ? ((CheckBox)ctrl).IsChecked.ToString() : "Null";
			lst.Add(ctrl);
			object ret = Registry.GetValue(keyPath, keyName, null);
			if(ret == null) {
				Registry.SetValue(keyPath, keyName, ctrlVal);
			} else {
				string readRegVal = ret.ToString();
				if(readRegVal.Length == 0)
					Registry.SetValue(keyPath, keyName, ctrlVal);
				else {
					switch(readRegVal) {
					case "True":
						ctrl.IsChecked = true;
						break;
					case "False":
						ctrl.IsChecked = false;
						break;
					case "Null":
						ctrl.IsChecked = null;
						break;
					default:
						Registry.SetValue(keyPath, keyName, ctrlVal);
						Console.WriteLine("Wrong case: " + readRegVal);
						break;
					}
				}
			}
		} // ///////////////////////////////////////////////////////////////////////////////////
	} // ***************************************************************************************
}
