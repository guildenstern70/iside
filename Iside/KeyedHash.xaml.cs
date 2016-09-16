﻿/**
    Iside - .NET WPF Version 
    Copyright (C) LittleLite Software
    Author Alessio Saltarin
**/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using IsideLogic;
using LLCryptoLib.Utils;

namespace Iside
{

    /// <summary>
    /// Interaction logic for KeyedHash.xaml
    /// </summary>
    public partial class KeyedHash : Window
    {
        private HexEnum style;
        private string strBytes;
        private Cursor enterCursor;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyedHash" /> class.
        /// </summary>
        public KeyedHash()
        {
            InitializeComponent();
            this.style = HexEnum.SPACE; // TO DO PERSIST WITH SETTINGS
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="IsideException"></exception>
        public byte[] GetKey()
        {
            byte[] bytes = null;

            if (strBytes == null)
            {
                throw new IsideException("Null input string!");
            }
            else
            {
                if (strBytes.Length == 0)
                {
                    throw new IsideException("Zero input string!");
                }
            }

            if (this.radHex.IsChecked.Value)
            {
                bytes = Hexer.Hex2Bytes(strBytes);
            }
            else
            {
                bytes = Hexer.Text2Bytes(strBytes);
            }

            if (bytes != null)
            {
                System.Diagnostics.Debug.WriteLine("Bytes are: " + Hexer.BytesToHex(bytes, HexEnum.UNIX));
            }
            else
            {
                throw new IsideException("Cannot translate bytes!");
            }

            return bytes;
        }

        private bool ValidateInputText()
        {
            bool textOk = true;

            if (this.txtHashKey.Text.Length > 0)
            {
                this.strBytes = this.txtHashKey.Text.Trim();

                if (this.radHex.IsChecked.Value)
                {
                    if (!Hexer.IsHex(this.strBytes, this.style))
                    {
                        StringBuilder sb = new StringBuilder("ff0012ac4a");
                        HexStyler hs = new HexStyler(this.style);
                        string exampleString = hs.Format(sb.ToString());
                        MessageBox.Show(this, "Input text seems not to be hexadecimal (ie: \"" + exampleString + "\")", IsideLogic.Config.APPNAME, MessageBoxButton.OK, MessageBoxImage.Warning);
                        textOk = false;
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "Enter a key in text or hexadecimal format", IsideLogic.Config.APPNAME, MessageBoxButton.OK, MessageBoxImage.Error);
                textOk = false;
            }

            return textOk;
        }

        private void Hex2TextChange(bool toHex)
        {
            string inText = this.txtHashKey.Text;
            if (inText.Length > 0)
            {
                if (toHex)
                {
                    if (!Hexer.IsHex(inText, this.style))
                    {
                        this.txtHashKey.Text = this.ToHex(inText);
                    }
                }
                else
                {
                    if (Hexer.IsHex(inText, this.style))
                    {
                        this.txtHashKey.Text = Hexer.Hex2Text(inText, this.style);
                    }
                }
            }
        }

        private string ToHex(string text)
        {
            return Hexer.Text2Hex(text, this.style);
        }

        private void radText_Checked(object sender, RoutedEventArgs e)
        {
            this.Hex2TextChange(false);
        }

        private void radHex_Checked(object sender, RoutedEventArgs e)
        {
            this.Hex2TextChange(true);
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!this.ValidateInputText())
            {
                e.Cancel = true;
            }

            if (this.enterCursor != null)
            {
                Mouse.OverrideCursor = this.enterCursor;
            }
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.enterCursor = this.Cursor;
            Mouse.OverrideCursor = null;
        }
    }
}
