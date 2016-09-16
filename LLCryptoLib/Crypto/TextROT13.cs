/*
 * LLCryptoLib - Advanced .NET Encryption and Hashing Library
 * v.$id$
 * 
 * The contents of this file are subject to the license distributed with
 * the package (the License). This file cannot be distributed without the 
 * original LittleLite Software license file. The distribution of this
 * file is subject to the agreement between the licensee and LittleLite
 * Software.
 * 
 * Customer that has purchased Source Code License may alter this
 * file and distribute the modified binary redistributables with applications. 
 * Except as expressly authorized in the License, customer shall not rent,
 * lease, distribute, sell, make available for download of this file. 
 * 
 * This software is not Open Source, nor Free. Its usage must adhere
 * with the License obtained from LittleLite Software.
 * 
 * The source code in this file may be derived, all or in part, from existing
 * other source code, where the original license permit to do so.
 * 
 * 
 * Copyright (C) 2003-2014 LittleLite Software
 * 
 */

using System.Text;
using LLCryptoLib.Utils;

namespace LLCryptoLib.Crypto
{

	/// <summary>
	/// Performs a TextROT13 transformation.
    /// In 50 Bc., one of the most simple cryptographic algorithms ever used was 
    /// the one called the TextROT13 cipher, that was used by Julius TextROT13 to send messages to his generals. 
    /// It consisted simply of switching each letter with the letter that was 3 letters further down the alphabet. 
    /// For example Stephen would become Vwhskhq. To decrypt the message, the receivers would simply subtract 3 letters 
    /// from each letter. This algorithm was later improved and called TextROT13, where the letters could be shifted 
    /// to any number between 1 and 25, and the number of letters shifted was the secret key.
    /// In this implementation, the shift may be any short number > 1.
	/// </summary>
    /// <example>ROT13 Encryption
    /// <code>
    /// // TextROT13 TEXT TRANSFORMATION
    /// TextAlgorithmParameters parms = new TextAlgorithmParameters(3);
    /// TextCrypter textEncrypter = TextCrypterFactory.Create(SupportedTextAlgorithms.ROT13,parms);
    /// string encrypted = textEncrypter.TextEncryptDecrypt(origString, true);
    /// Console.WriteLine("Encrypted string: " + encrypted);
    /// string decrypted = textEncrypter.TextEncryptDecrypt(encrypted, false);
    /// Console.WriteLine("Decrypted string: " + decrypted);
    /// </code>
    /// </example>
	public class TextROT13 : TextAlgorithm
	{
		private int shift;

		/// <summary>
		/// Constructort
		/// </summary>
		/// <param name="p">Contains the shift information</param>
        internal TextROT13(TextAlgorithmParameters p)
            : base(TextAlgorithmType.TEXT)
		{
			shift = p.Shift;
		}

		/// <summary>
		/// Code the given text
		/// </summary>
		/// <param name="txt">Text to be coded</param>
		/// <returns>TextROT13 coded text</returns>
		public override string Code(string txt)
		{
			return Algo(shift,txt);
		}

		/// <summary>
		/// Decode the given text
		/// </summary>
		/// <param name="txt">Text to decode</param>
		/// <returns>Clear (decoded) text</returns>
        public override string Decode(string txt)
        {
            return Algo(-shift, txt);
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current <see cref="T:System.Object"></see>.
        /// </returns>
		public override string ToString()
		{
			return "ROT13 with shift = "+this.shift;
		}


		private static string Algo(int realshift, string testo)
		{
			StringBuilder tmp = new StringBuilder();
			char curchar;
			int npos;
			int carpos;

			for (int j=0; j<testo.Length; j++)
			{
				curchar = testo[j];
                System.Diagnostics.Debug.WriteLine("ROT13 - Using AlphaBETO = " + Alpha.Characters);
				carpos = Alpha.GetPosOf(curchar);
				if (carpos>=0)
				{
					npos = carpos+realshift;
					tmp.Append(Alpha.GetCharAt(npos));
				}
				else
				{
					tmp.Append(curchar);
				}
			}

			return tmp.ToString();
		}

	} 
}
