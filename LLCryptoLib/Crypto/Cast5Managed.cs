﻿using System;
using System.Security.Cryptography;

namespace LLCryptoLib.Crypto
{
    internal class Cast5
    {
        private static unsafe uint* S1;
        private static unsafe uint* S2;
        private static unsafe uint* S3;
        private static unsafe uint* S4;

        private Cast5()
        {
        }

        private static unsafe void IntScheduleKey(byte[] keyBytes, uint* K)
        {
            #region Prepare subkeys

            fixed (uint* S5 = &S5_SBox[0], S6 = &S6_SBox[0], S7 = &S7_SBox[0], S8 = &S8_SBox[0])
            {
                uint a, b, c, d;
                fixed (byte* p = &keyBytes[0])
                {
                    uint* w = (uint*) p;
                    a = w[0];
                    b = w[1];
                    c = w[2];
                    d = w[3];
                }
                uint e = a ^ S5[(d >> 16) & 0xFF] ^ S6[d & 0xFF] ^ S7[d >> 24] ^ S8[(d >> 8) & 0xFF] ^ S7[c >> 24];
                uint f = c ^ S5[e >> 24] ^ S6[(e >> 8) & 0xFF] ^ S7[(e >> 16) & 0xFF] ^ S8[e & 0xFF] ^
                         S8[(c >> 8) & 0xFF];
                uint g = d ^ S5[f & 0xFF] ^ S6[(f >> 8) & 0xFF] ^ S7[(f >> 16) & 0xFF] ^ S8[f >> 24] ^
                         S5[(c >> 16) & 0xFF];
                uint h = b ^ S5[(g >> 8) & 0xFF] ^ S6[(g >> 16) & 0xFF] ^ S7[g & 0xFF] ^ S8[g >> 24] ^ S6[c & 0xFF];
                K[0] = S5[g >> 24] ^ S6[(g >> 16) & 0xFF] ^ S7[f & 0xFF] ^ S8[(f >> 8) & 0xFF] ^ S5[(e >> 8) & 0xFF];
                K[1] = S5[(g >> 8) & 0xFF] ^ S6[g & 0xFF] ^ S7[(f >> 16) & 0xFF] ^ S8[f >> 24] ^ S6[(f >> 8) & 0xFF];
                K[2] = S5[h >> 24] ^ S6[(h >> 16) & 0xFF] ^ S7[e & 0xFF] ^ S8[(e >> 8) & 0xFF] ^ S7[(g >> 16) & 0xFF];
                K[3] = S5[(h >> 8) & 0xFF] ^ S6[h & 0xFF] ^ S7[(e >> 16) & 0xFF] ^ S8[e >> 24] ^ S8[h >> 24];
                a = g ^ S5[(f >> 16) & 0xFF] ^ S6[f & 0xFF] ^ S7[f >> 24] ^ S8[(f >> 8) & 0xFF] ^ S7[e >> 24];
                b = e ^ S5[a >> 24] ^ S6[(a >> 8) & 0xFF] ^ S7[(a >> 16) & 0xFF] ^ S8[a & 0xFF] ^ S8[(e >> 8) & 0xFF];
                c = f ^ S5[b & 0xFF] ^ S6[(b >> 8) & 0xFF] ^ S7[(b >> 16) & 0xFF] ^ S8[b >> 24] ^ S5[(e >> 16) & 0xFF];
                d = h ^ S5[(c >> 8) & 0xFF] ^ S6[(c >> 16) & 0xFF] ^ S7[c & 0xFF] ^ S8[c >> 24] ^ S6[e & 0xFF];
                K[4] = S5[a & 0xFF] ^ S6[(a >> 8) & 0xFF] ^ S7[d >> 24] ^ S8[(d >> 16) & 0xFF] ^ S5[c >> 24];
                K[5] = S5[(a >> 16) & 0xFF] ^ S6[a >> 24] ^ S7[(d >> 8) & 0xFF] ^ S8[d & 0xFF] ^ S6[(d >> 16) & 0xFF];
                K[6] = S5[b & 0xFF] ^ S6[(b >> 8) & 0xFF] ^ S7[c >> 24] ^ S8[(c >> 16) & 0xFF] ^ S7[a & 0xFF];
                K[7] = S5[(b >> 16) & 0xFF] ^ S6[b >> 24] ^ S7[(c >> 8) & 0xFF] ^ S8[c & 0xFF] ^ S8[b & 0xFF];
                e = a ^ S5[(d >> 16) & 0xFF] ^ S6[d & 0xFF] ^ S7[d >> 24] ^ S8[(d >> 8) & 0xFF] ^ S7[c >> 24];
                f = c ^ S5[e >> 24] ^ S6[(e >> 8) & 0xFF] ^ S7[(e >> 16) & 0xFF] ^ S8[e & 0xFF] ^ S8[(c >> 8) & 0xFF];
                g = d ^ S5[f & 0xFF] ^ S6[(f >> 8) & 0xFF] ^ S7[(f >> 16) & 0xFF] ^ S8[f >> 24] ^ S5[(c >> 16) & 0xFF];
                h = b ^ S5[(g >> 8) & 0xFF] ^ S6[(g >> 16) & 0xFF] ^ S7[g & 0xFF] ^ S8[g >> 24] ^ S6[c & 0xFF];
                K[8] = S5[e & 0xFF] ^ S6[(e >> 8) & 0xFF] ^ S7[h >> 24] ^ S8[(h >> 16) & 0xFF] ^ S5[(g >> 16) & 0xFF];
                K[9] = S5[(e >> 16) & 0xFF] ^ S6[e >> 24] ^ S7[(h >> 8) & 0xFF] ^ S8[h & 0xFF] ^ S6[h >> 24];
                K[10] = S5[f & 0xFF] ^ S6[(f >> 8) & 0xFF] ^ S7[g >> 24] ^ S8[(g >> 16) & 0xFF] ^ S7[(e >> 8) & 0xFF];
                K[11] = S5[(f >> 16) & 0xFF] ^ S6[f >> 24] ^ S7[(g >> 8) & 0xFF] ^ S8[g & 0xFF] ^ S8[(f >> 8) & 0xFF];
                a = g ^ S5[(f >> 16) & 0xFF] ^ S6[f & 0xFF] ^ S7[f >> 24] ^ S8[(f >> 8) & 0xFF] ^ S7[e >> 24];
                b = e ^ S5[a >> 24] ^ S6[(a >> 8) & 0xFF] ^ S7[(a >> 16) & 0xFF] ^ S8[a & 0xFF] ^ S8[(e >> 8) & 0xFF];
                c = f ^ S5[b & 0xFF] ^ S6[(b >> 8) & 0xFF] ^ S7[(b >> 16) & 0xFF] ^ S8[b >> 24] ^ S5[(e >> 16) & 0xFF];
                d = h ^ S5[(c >> 8) & 0xFF] ^ S6[(c >> 16) & 0xFF] ^ S7[c & 0xFF] ^ S8[c >> 24] ^ S6[e & 0xFF];
                K[12] = S5[c >> 24] ^ S6[(c >> 16) & 0xFF] ^ S7[b & 0xFF] ^ S8[(b >> 8) & 0xFF] ^ S5[a & 0xFF];
                K[13] = S5[(c >> 8) & 0xFF] ^ S6[c & 0xFF] ^ S7[(b >> 16) & 0xFF] ^ S8[b >> 24] ^ S6[b & 0xFF];
                K[14] = S5[d >> 24] ^ S6[(d >> 16) & 0xFF] ^ S7[a & 0xFF] ^ S8[(a >> 8) & 0xFF] ^ S7[c >> 24];
                K[15] = S5[(d >> 8) & 0xFF] ^ S6[d & 0xFF] ^ S7[(a >> 16) & 0xFF] ^ S8[a >> 24] ^ S8[(d >> 16) & 0xFF];
                e = a ^ S5[(d >> 16) & 0xFF] ^ S6[d & 0xFF] ^ S7[d >> 24] ^ S8[(d >> 8) & 0xFF] ^ S7[c >> 24];
                f = c ^ S5[e >> 24] ^ S6[(e >> 8) & 0xFF] ^ S7[(e >> 16) & 0xFF] ^ S8[e & 0xFF] ^ S8[(c >> 8) & 0xFF];
                g = d ^ S5[f & 0xFF] ^ S6[(f >> 8) & 0xFF] ^ S7[(f >> 16) & 0xFF] ^ S8[f >> 24] ^ S5[(c >> 16) & 0xFF];
                h = b ^ S5[(g >> 8) & 0xFF] ^ S6[(g >> 16) & 0xFF] ^ S7[g & 0xFF] ^ S8[g >> 24] ^ S6[c & 0xFF];
                K[16] = S5[g >> 24] ^ S6[(g >> 16) & 0xFF] ^ S7[f & 0xFF] ^ S8[(f >> 8) & 0xFF] ^ S5[(e >> 8) & 0xFF];
                K[17] = S5[(g >> 8) & 0xFF] ^ S6[g & 0xFF] ^ S7[(f >> 16) & 0xFF] ^ S8[f >> 24] ^ S6[(f >> 8) & 0xFF];
                K[18] = S5[h >> 24] ^ S6[(h >> 16) & 0xFF] ^ S7[e & 0xFF] ^ S8[(e >> 8) & 0xFF] ^ S7[(g >> 16) & 0xFF];
                K[19] = S5[(h >> 8) & 0xFF] ^ S6[h & 0xFF] ^ S7[(e >> 16) & 0xFF] ^ S8[e >> 24] ^ S8[h >> 24];
                a = g ^ S5[(f >> 16) & 0xFF] ^ S6[f & 0xFF] ^ S7[f >> 24] ^ S8[(f >> 8) & 0xFF] ^ S7[e >> 24];
                b = e ^ S5[a >> 24] ^ S6[(a >> 8) & 0xFF] ^ S7[(a >> 16) & 0xFF] ^ S8[a & 0xFF] ^ S8[(e >> 8) & 0xFF];
                c = f ^ S5[b & 0xFF] ^ S6[(b >> 8) & 0xFF] ^ S7[(b >> 16) & 0xFF] ^ S8[b >> 24] ^ S5[(e >> 16) & 0xFF];
                d = h ^ S5[(c >> 8) & 0xFF] ^ S6[(c >> 16) & 0xFF] ^ S7[c & 0xFF] ^ S8[c >> 24] ^ S6[e & 0xFF];
                K[20] = S5[a & 0xFF] ^ S6[(a >> 8) & 0xFF] ^ S7[d >> 24] ^ S8[(d >> 16) & 0xFF] ^ S5[c >> 24];
                K[21] = S5[(a >> 16) & 0xFF] ^ S6[a >> 24] ^ S7[(d >> 8) & 0xFF] ^ S8[d & 0xFF] ^ S6[(d >> 16) & 0xFF];
                K[22] = S5[b & 0xFF] ^ S6[(b >> 8) & 0xFF] ^ S7[c >> 24] ^ S8[(c >> 16) & 0xFF] ^ S7[a & 0xFF];
                K[23] = S5[(b >> 16) & 0xFF] ^ S6[b >> 24] ^ S7[(c >> 8) & 0xFF] ^ S8[c & 0xFF] ^ S8[b & 0xFF];
                e = a ^ S5[(d >> 16) & 0xFF] ^ S6[d & 0xFF] ^ S7[d >> 24] ^ S8[(d >> 8) & 0xFF] ^ S7[c >> 24];
                f = c ^ S5[e >> 24] ^ S6[(e >> 8) & 0xFF] ^ S7[(e >> 16) & 0xFF] ^ S8[e & 0xFF] ^ S8[(c >> 8) & 0xFF];
                g = d ^ S5[f & 0xFF] ^ S6[(f >> 8) & 0xFF] ^ S7[(f >> 16) & 0xFF] ^ S8[f >> 24] ^ S5[(c >> 16) & 0xFF];
                h = b ^ S5[(g >> 8) & 0xFF] ^ S6[(g >> 16) & 0xFF] ^ S7[g & 0xFF] ^ S8[g >> 24] ^ S6[c & 0xFF];
                K[24] = S5[e & 0xFF] ^ S6[(e >> 8) & 0xFF] ^ S7[h >> 24] ^ S8[(h >> 16) & 0xFF] ^ S5[(g >> 16) & 0xFF];
                K[25] = S5[(e >> 16) & 0xFF] ^ S6[e >> 24] ^ S7[(h >> 8) & 0xFF] ^ S8[h & 0xFF] ^ S6[h >> 24];
                K[26] = S5[f & 0xFF] ^ S6[(f >> 8) & 0xFF] ^ S7[g >> 24] ^ S8[(g >> 16) & 0xFF] ^ S7[(e >> 8) & 0xFF];
                K[27] = S5[(f >> 16) & 0xFF] ^ S6[f >> 24] ^ S7[(g >> 8) & 0xFF] ^ S8[g & 0xFF] ^ S8[(f >> 8) & 0xFF];
                a = g ^ S5[(f >> 16) & 0xFF] ^ S6[f & 0xFF] ^ S7[f >> 24] ^ S8[(f >> 8) & 0xFF] ^ S7[e >> 24];
                b = e ^ S5[a >> 24] ^ S6[(a >> 8) & 0xFF] ^ S7[(a >> 16) & 0xFF] ^ S8[a & 0xFF] ^ S8[(e >> 8) & 0xFF];
                c = f ^ S5[b & 0xFF] ^ S6[(b >> 8) & 0xFF] ^ S7[(b >> 16) & 0xFF] ^ S8[b >> 24] ^ S5[(e >> 16) & 0xFF];
                d = h ^ S5[(c >> 8) & 0xFF] ^ S6[(c >> 16) & 0xFF] ^ S7[c & 0xFF] ^ S8[c >> 24] ^ S6[e & 0xFF];
                K[28] = S5[c >> 24] ^ S6[(c >> 16) & 0xFF] ^ S7[b & 0xFF] ^ S8[(b >> 8) & 0xFF] ^ S5[a & 0xFF];
                K[29] = S5[(c >> 8) & 0xFF] ^ S6[c & 0xFF] ^ S7[(b >> 16) & 0xFF] ^ S8[b >> 24] ^ S6[b & 0xFF];
                K[30] = S5[d >> 24] ^ S6[(d >> 16) & 0xFF] ^ S7[a & 0xFF] ^ S8[(a >> 8) & 0xFF] ^ S7[c >> 24];
                K[31] = S5[(d >> 8) & 0xFF] ^ S6[d & 0xFF] ^ S7[(a >> 16) & 0xFF] ^ S8[a >> 24] ^ S8[(d >> 16) & 0xFF];
            }

            #endregion
        }

        private static unsafe void EncryptBlock(ref uint L, ref uint R, uint* K)
        {
            unchecked
            {
                uint Ri = K[0] + R;
                int r = (int) K[16];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = ((S1[Ri >> 24] ^ S2[(Ri >> 16) & 0xFF]) - S3[(Ri >> 8) & 0xFF] + S4[Ri & 0xFF]) ^ L;
                L = R;
                R = Ri;
                Ri = K[1] ^ Ri;
                r = (int) K[17];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = (S1[Ri >> 24] - S2[(Ri >> 16) & 0xFF] + S3[(Ri >> 8) & 0xFF]) ^ S4[Ri & 0xFF] ^ L;
                L = R;
                R = Ri;
                Ri = K[2] - Ri;
                r = (int) K[18];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = (((S1[Ri >> 24] + S2[(Ri >> 16) & 0xFF]) ^ S3[(Ri >> 8) & 0xFF]) - S4[Ri & 0xFF]) ^ L;
                L = R;
                R = Ri;
                Ri = K[3] + Ri;
                r = (int) K[19];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = ((S1[Ri >> 24] ^ S2[(Ri >> 16) & 0xFF]) - S3[(Ri >> 8) & 0xFF] + S4[Ri & 0xFF]) ^ L;
                L = R;
                R = Ri;
                Ri = K[4] ^ Ri;
                r = (int) K[20];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = (S1[Ri >> 24] - S2[(Ri >> 16) & 0xFF] + S3[(Ri >> 8) & 0xFF]) ^ S4[Ri & 0xFF] ^ L;
                L = R;
                R = Ri;
                Ri = K[5] - Ri;
                r = (int) K[21];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = (((S1[Ri >> 24] + S2[(Ri >> 16) & 0xFF]) ^ S3[(Ri >> 8) & 0xFF]) - S4[Ri & 0xFF]) ^ L;
                L = R;
                R = Ri;
                Ri = K[6] + Ri;
                r = (int) K[22];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = ((S1[Ri >> 24] ^ S2[(Ri >> 16) & 0xFF]) - S3[(Ri >> 8) & 0xFF] + S4[Ri & 0xFF]) ^ L;
                L = R;
                R = Ri;
                Ri = K[7] ^ Ri;
                r = (int) K[23];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = (S1[Ri >> 24] - S2[(Ri >> 16) & 0xFF] + S3[(Ri >> 8) & 0xFF]) ^ S4[Ri & 0xFF] ^ L;
                L = R;
                R = Ri;
                Ri = K[8] - Ri;
                r = (int) K[24];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = (((S1[Ri >> 24] + S2[(Ri >> 16) & 0xFF]) ^ S3[(Ri >> 8) & 0xFF]) - S4[Ri & 0xFF]) ^ L;
                L = R;
                R = Ri;
                Ri = K[9] + Ri;
                r = (int) K[25];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = ((S1[Ri >> 24] ^ S2[(Ri >> 16) & 0xFF]) - S3[(Ri >> 8) & 0xFF] + S4[Ri & 0xFF]) ^ L;
                L = R;
                R = Ri;
                Ri = K[10] ^ Ri;
                r = (int) K[26];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = (S1[Ri >> 24] - S2[(Ri >> 16) & 0xFF] + S3[(Ri >> 8) & 0xFF]) ^ S4[Ri & 0xFF] ^ L;
                L = R;
                R = Ri;
                Ri = K[11] - Ri;
                r = (int) K[27];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = (((S1[Ri >> 24] + S2[(Ri >> 16) & 0xFF]) ^ S3[(Ri >> 8) & 0xFF]) - S4[Ri & 0xFF]) ^ L;
                L = R;
                R = Ri;
                Ri = K[12] + Ri;
                r = (int) K[28];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = ((S1[Ri >> 24] ^ S2[(Ri >> 16) & 0xFF]) - S3[(Ri >> 8) & 0xFF] + S4[Ri & 0xFF]) ^ L;
                L = R;
                R = Ri;
                Ri = K[13] ^ Ri;
                r = (int) K[29];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = (S1[Ri >> 24] - S2[(Ri >> 16) & 0xFF] + S3[(Ri >> 8) & 0xFF]) ^ S4[Ri & 0xFF] ^ L;
                L = R;
                R = Ri;
                Ri = K[14] - Ri;
                r = (int) K[30];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = (((S1[Ri >> 24] + S2[(Ri >> 16) & 0xFF]) ^ S3[(Ri >> 8) & 0xFF]) - S4[Ri & 0xFF]) ^ L;
                L = R;
                R = Ri;
                Ri = K[15] + Ri;
                r = (int) K[31];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = ((S1[Ri >> 24] ^ S2[(Ri >> 16) & 0xFF]) - S3[(Ri >> 8) & 0xFF] + S4[Ri & 0xFF]) ^ L;
                L = Ri;
            }
        }

        private static unsafe void DecryptBlock(ref uint L, ref uint R, uint* K)
        {
            unchecked
            {
                uint Ri = K[15] + R;
                int r = (int) K[31];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = ((S1[Ri >> 24] ^ S2[(Ri >> 16) & 0xFF]) - S3[(Ri >> 8) & 0xFF] + S4[Ri & 0xFF]) ^ L;
                L = R;
                R = Ri;
                Ri = K[14] - Ri;
                r = (int) K[30];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = (((S1[Ri >> 24] + S2[(Ri >> 16) & 0xFF]) ^ S3[(Ri >> 8) & 0xFF]) - S4[Ri & 0xFF]) ^ L;
                L = R;
                R = Ri;
                Ri = K[13] ^ Ri;
                r = (int) K[29];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = (S1[Ri >> 24] - S2[(Ri >> 16) & 0xFF] + S3[(Ri >> 8) & 0xFF]) ^ S4[Ri & 0xFF] ^ L;
                L = R;
                R = Ri;
                Ri = K[12] + Ri;
                r = (int) K[28];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = ((S1[Ri >> 24] ^ S2[(Ri >> 16) & 0xFF]) - S3[(Ri >> 8) & 0xFF] + S4[Ri & 0xFF]) ^ L;
                L = R;
                R = Ri;
                Ri = K[11] - Ri;
                r = (int) K[27];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = (((S1[Ri >> 24] + S2[(Ri >> 16) & 0xFF]) ^ S3[(Ri >> 8) & 0xFF]) - S4[Ri & 0xFF]) ^ L;
                L = R;
                R = Ri;
                Ri = K[10] ^ Ri;
                r = (int) K[26];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = (S1[Ri >> 24] - S2[(Ri >> 16) & 0xFF] + S3[(Ri >> 8) & 0xFF]) ^ S4[Ri & 0xFF] ^ L;
                L = R;
                R = Ri;
                Ri = K[9] + Ri;
                r = (int) K[25];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = ((S1[Ri >> 24] ^ S2[(Ri >> 16) & 0xFF]) - S3[(Ri >> 8) & 0xFF] + S4[Ri & 0xFF]) ^ L;
                L = R;
                R = Ri;
                Ri = K[8] - Ri;
                r = (int) K[24];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = (((S1[Ri >> 24] + S2[(Ri >> 16) & 0xFF]) ^ S3[(Ri >> 8) & 0xFF]) - S4[Ri & 0xFF]) ^ L;
                L = R;
                R = Ri;
                Ri = K[7] ^ Ri;
                r = (int) K[23];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = (S1[Ri >> 24] - S2[(Ri >> 16) & 0xFF] + S3[(Ri >> 8) & 0xFF]) ^ S4[Ri & 0xFF] ^ L;
                L = R;
                R = Ri;
                Ri = K[6] + Ri;
                r = (int) K[22];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = ((S1[Ri >> 24] ^ S2[(Ri >> 16) & 0xFF]) - S3[(Ri >> 8) & 0xFF] + S4[Ri & 0xFF]) ^ L;
                L = R;
                R = Ri;
                Ri = K[5] - Ri;
                r = (int) K[21];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = (((S1[Ri >> 24] + S2[(Ri >> 16) & 0xFF]) ^ S3[(Ri >> 8) & 0xFF]) - S4[Ri & 0xFF]) ^ L;
                L = R;
                R = Ri;
                Ri = K[4] ^ Ri;
                r = (int) K[20];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = (S1[Ri >> 24] - S2[(Ri >> 16) & 0xFF] + S3[(Ri >> 8) & 0xFF]) ^ S4[Ri & 0xFF] ^ L;
                L = R;
                R = Ri;
                Ri = K[3] + Ri;
                r = (int) K[19];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = ((S1[Ri >> 24] ^ S2[(Ri >> 16) & 0xFF]) - S3[(Ri >> 8) & 0xFF] + S4[Ri & 0xFF]) ^ L;
                L = R;
                R = Ri;
                Ri = K[2] - Ri;
                r = (int) K[18];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = (((S1[Ri >> 24] + S2[(Ri >> 16) & 0xFF]) ^ S3[(Ri >> 8) & 0xFF]) - S4[Ri & 0xFF]) ^ L;
                L = R;
                R = Ri;
                Ri = K[1] ^ Ri;
                r = (int) K[17];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = (S1[Ri >> 24] - S2[(Ri >> 16) & 0xFF] + S3[(Ri >> 8) & 0xFF]) ^ S4[Ri & 0xFF] ^ L;
                L = R;
                R = Ri;
                Ri = K[0] + Ri;
                r = (int) K[16];
                Ri = (Ri << r) | (Ri >> (32 - r));
                Ri = ((S1[Ri >> 24] ^ S2[(Ri >> 16) & 0xFF]) - S3[(Ri >> 8) & 0xFF] + S4[Ri & 0xFF]) ^ L;
                L = Ri;
            }
        }

        public static unsafe int[] ScheduleKey(byte[] keyBytes)
        {
            int[] result = new int[32];
            fixed (int* K = &result[0])
                IntScheduleKey(keyBytes, (uint*) K);
            return result;
        }

        //public static unsafe void ScheduleKey(byte[] keyBytes, int[] key)
        //{
        //    fixed (int* K = &key[0])
        //        IntScheduleKey(keyBytes, (uint*)K);
        //}

        private static unsafe void EncryptECB(int[] key, byte* bytes)
        {
            fixed (int* K = &key[0])
            fixed (uint* pS1 = &S1_SBox[0], pS2 = &S2_SBox[0], pS3 = &S3_SBox[0], pS4 = &S4_SBox[0])
            {
                uint L = *(uint*) bytes;
                uint R = *(uint*) (bytes + 4);
                S1 = pS1;
                S2 = pS2;
                S3 = pS3;
                S4 = pS4;
                EncryptBlock(ref L, ref R, (uint*) K);
                *(uint*) bytes = L;
                *(uint*) (bytes + 4) = R;
            }
        }

        public static unsafe void EncryptECB(int[] key, byte[] bytes, int offset)
        {
            fixed (byte* pBytes = &bytes[offset])
                EncryptECB(key, pBytes);
        }

        private static unsafe void DecryptECB(int[] key, byte* bytes)
        {
            fixed (int* K = &key[0])
            fixed (uint* pS1 = &S1_SBox[0], pS2 = &S2_SBox[0], pS3 = &S3_SBox[0], pS4 = &S4_SBox[0])
            {
                uint L = *(uint*) bytes;
                uint R = *(uint*) (bytes + 4);
                S1 = pS1;
                S2 = pS2;
                S3 = pS3;
                S4 = pS4;
                DecryptBlock(ref L, ref R, (uint*) K);
                *(uint*) bytes = L;
                *(uint*) (bytes + 4) = R;
            }
        }

        public static unsafe void DecryptECB(int[] key, byte[] bytes, int offset)
        {
            fixed (byte* pBytes = &bytes[offset])
                DecryptECB(key, pBytes);
        }

        public static unsafe long GetOrdinaryIV(int[] key)
        {
            fixed (int* K = &key[0])
            fixed (uint* pS1 = &S1_SBox[0], pS2 = &S2_SBox[0], pS3 = &S3_SBox[0], pS4 = &S4_SBox[0])
            {
                uint L = 0;
                uint R = 0;
                S1 = pS1;
                S2 = pS2;
                S3 = pS3;
                S4 = pS4;
                EncryptBlock(ref L, ref R, (uint*) K);
                return (long) (((ulong) R << 32) | L);
            }
        }

        private static unsafe long EncryptCBC(int[] key, byte* bytes, int blockCount, long iv)
        {
            fixed (int* K = &key[0])
            fixed (uint* pS1 = &S1_SBox[0], pS2 = &S2_SBox[0], pS3 = &S3_SBox[0], pS4 = &S4_SBox[0])
            {
                uint L = (uint) (ulong) iv;
                uint R = (uint) ((ulong) iv >> 32);
                S1 = pS1;
                S2 = pS2;
                S3 = pS3;
                S4 = pS4;
                do
                {
                    L ^= *(uint*) bytes;
                    R ^= *(uint*) (bytes + 4);
                    EncryptBlock(ref L, ref R, (uint*) K);
                    *(uint*) bytes = L;
                    *(uint*) (bytes + 4) = R;
                    bytes += 8;
                } while (--blockCount != 0);
                return (long) (((ulong) R << 32) | L);
            }
        }

        public static unsafe long EncryptCBC(int[] key, byte[] bytes, int offset, int length, long iv)
        {
            if ((length & 7) != 0)
                throw new ArgumentException("The \"length\" must be a multiple of the block size (8 bytes).", "length");
            if (length > 0)
                fixed (byte* pBytes = &bytes[offset])
                    return EncryptCBC(key, pBytes, length >> 3, iv);
            return iv;
        }

        private static unsafe long DecryptCBC(int[] key, byte* bytes, int blockCount, long iv)
        {
            fixed (int* K = &key[0])
            fixed (uint* pS1 = &S1_SBox[0], pS2 = &S2_SBox[0], pS3 = &S3_SBox[0], pS4 = &S4_SBox[0])
            {
                uint Lnext = (uint) (ulong) iv;
                uint Rnext = (uint) ((ulong) iv >> 32);
                S1 = pS1;
                S2 = pS2;
                S3 = pS3;
                S4 = pS4;
                do
                {
                    uint L = *(uint*) bytes;
                    uint R = *(uint*) (bytes + 4);
                    DecryptBlock(ref L, ref R, (uint*) K);
                    L ^= Lnext;
                    R ^= Rnext;
                    Lnext = *(uint*) bytes;
                    Rnext = *(uint*) (bytes + 4);
                    *(uint*) bytes = L;
                    *(uint*) (bytes + 4) = R;
                    bytes += 8;
                } while (--blockCount != 0);
                return (long) (((ulong) Rnext << 32) | Lnext);
            }
        }

        public static unsafe long DecryptCBC(int[] key, byte[] bytes, int offset, int length, long iv)
        {
            if ((length & 7) != 0)
                throw new ArgumentException("The \"length\" must be a multiple of the block size (8 bytes).", "length");
            if (length > 0)
                fixed (byte* pBytes = &bytes[offset])
                    return DecryptCBC(key, pBytes, length >> 3, iv);
            return iv;
        }

        public static void ClearKey(int[] key)
        {
            Array.Clear(key, 0, 32);
        }

        #region S5 - S8 S-Boxes

        private static readonly uint[] S5_SBox =
        {
            0x7ec90c04u, 0x2c6e74b9u, 0x9b0e66dfu, 0xa6337911u, 0xb86a7fffu, 0x1dd358f5u, 0x44dd9d44u, 0x1731167fu,
            0x08fbf1fau, 0xe7f511ccu, 0xd2051b00u, 0x735aba00u, 0x2ab722d8u, 0x386381cbu, 0xacf6243au, 0x69befd7au,
            0xe6a2e77fu, 0xf0c720cdu, 0xc4494816u, 0xccf5c180u, 0x38851640u, 0x15b0a848u, 0xe68b18cbu, 0x4caadeffu,
            0x5f480a01u, 0x0412b2aau, 0x259814fcu, 0x41d0efe2u, 0x4e40b48du, 0x248eb6fbu, 0x8dba1cfeu, 0x41a99b02u,
            0x1a550a04u, 0xba8f65cbu, 0x7251f4e7u, 0x95a51725u, 0xc106ecd7u, 0x97a5980au, 0xc539b9aau, 0x4d79fe6au,
            0xf2f3f763u, 0x68af8040u, 0xed0c9e56u, 0x11b4958bu, 0xe1eb5a88u, 0x8709e6b0u, 0xd7e07156u, 0x4e29fea7u,
            0x6366e52du, 0x02d1c000u, 0xc4ac8e05u, 0x9377f571u, 0x0c05372au, 0x578535f2u, 0x2261be02u, 0xd642a0c9u,
            0xdf13a280u, 0x74b55bd2u, 0x682199c0u, 0xd421e5ecu, 0x53fb3ce8u, 0xc8adedb3u, 0x28a87fc9u, 0x3d959981u,
            0x5c1ff900u, 0xfe38d399u, 0x0c4eff0bu, 0x062407eau, 0xaa2f4fb1u, 0x4fb96976u, 0x90c79505u, 0xb0a8a774u,
            0xef55a1ffu, 0xe59ca2c2u, 0xa6b62d27u, 0xe66a4263u, 0xdf65001fu, 0x0ec50966u, 0xdfdd55bcu, 0x29de0655u,
            0x911e739au, 0x17af8975u, 0x32c7911cu, 0x89f89468u, 0x0d01e980u, 0x524755f4u, 0x03b63cc9u, 0x0cc844b2u,
            0xbcf3f0aau, 0x87ac36e9u, 0xe53a7426u, 0x01b3d82bu, 0x1a9e7449u, 0x64ee2d7eu, 0xcddbb1dau, 0x01c94910u,
            0xb868bf80u, 0x0d26f3fdu, 0x9342ede7u, 0x04a5c284u, 0x636737b6u, 0x50f5b616u, 0xf24766e3u, 0x8eca36c1u,
            0x136e05dbu, 0xfef18391u, 0xfb887a37u, 0xd6e7f7d4u, 0xc7fb7dc9u, 0x3063fcdfu, 0xb6f589deu, 0xec2941dau,
            0x26e46695u, 0xb7566419u, 0xf654efc5u, 0xd08d58b7u, 0x48925401u, 0xc1bacb7fu, 0xe5ff550fu, 0xb6083049u,
            0x5bb5d0e8u, 0x87d72e5au, 0xab6a6ee1u, 0x223a66ceu, 0xc62bf3cdu, 0x9e0885f9u, 0x68cb3e47u, 0x086c010fu,
            0xa21de820u, 0xd18b69deu, 0xf3f65777u, 0xfa02c3f6u, 0x407edac3u, 0xcbb3d550u, 0x1793084du, 0xb0d70ebau,
            0x0ab378d5u, 0xd951fb0cu, 0xded7da56u, 0x4124bbe4u, 0x94ca0b56u, 0x0f5755d1u, 0xe0e1e56eu, 0x6184b5beu,
            0x580a249fu, 0x94f74bc0u, 0xe327888eu, 0x9f7b5561u, 0xc3dc0280u, 0x05687715u, 0x646c6bd7u, 0x44904db3u,
            0x66b4f0a3u, 0xc0f1648au, 0x697ed5afu, 0x49e92ff6u, 0x309e374fu, 0x2cb6356au, 0x85808573u, 0x4991f840u,
            0x76f0ae02u, 0x083be84du, 0x28421c9au, 0x44489406u, 0x736e4cb8u, 0xc1092910u, 0x8bc95fc6u, 0x7d869cf4u,
            0x134f616fu, 0x2e77118du, 0xb31b2be1u, 0xaa90b472u, 0x3ca5d717u, 0x7d161bbau, 0x9cad9010u, 0xaf462ba2u,
            0x9fe459d2u, 0x45d34559u, 0xd9f2da13u, 0xdbc65487u, 0xf3e4f94eu, 0x176d486fu, 0x097c13eau, 0x631da5c7u,
            0x445f7382u, 0x175683f4u, 0xcdc66a97u, 0x70be0288u, 0xb3cdcf72u, 0x6e5dd2f3u, 0x20936079u, 0x459b80a5u,
            0xbe60e2dbu, 0xa9c23101u, 0xeba5315cu, 0x224e42f2u, 0x1c5c1572u, 0xf6721b2cu, 0x1ad2fff3u, 0x8c25404eu,
            0x324ed72fu, 0x4067b7fdu, 0x0523138eu, 0x5ca3bc78u, 0xdc0fd66eu, 0x75922283u, 0x784d6b17u, 0x58ebb16eu,
            0x44094f85u, 0x3f481d87u, 0xfcfeae7bu, 0x77b5ff76u, 0x8c2302bfu, 0xaaf47556u, 0x5f46b02au, 0x2b092801u,
            0x3d38f5f7u, 0x0ca81f36u, 0x52af4a8au, 0x66d5e7c0u, 0xdf3b0874u, 0x95055110u, 0x1b5ad7a8u, 0xf61ed5adu,
            0x6cf6e479u, 0x20758184u, 0xd0cefa65u, 0x88f7be58u, 0x4a046826u, 0x0ff6f8f3u, 0xa09c7f70u, 0x5346aba0u,
            0x5ce96c28u, 0xe176eda3u, 0x6bac307fu, 0x376829d2u, 0x85360fa9u, 0x17e3fe2au, 0x24b79767u, 0xf5a96b20u,
            0xd6cd2595u, 0x68ff1ebfu, 0x7555442cu, 0xf19f06beu, 0xf9e0659au, 0xeeb9491du, 0x34010718u, 0xbb30cab8u,
            0xe822fe15u, 0x88570983u, 0x750e6249u, 0xda627e55u, 0x5e76ffa8u, 0xb1534546u, 0x6d47de08u, 0xefe9e7d4u
        };

        private static readonly uint[] S6_SBox =
        {
            0xf6fa8f9du, 0x2cac6ce1u, 0x4ca34867u, 0xe2337f7cu, 0x95db08e7u, 0x016843b4u, 0xeced5cbcu, 0x325553acu,
            0xbf9f0960u, 0xdfa1e2edu, 0x83f0579du, 0x63ed86b9u, 0x1ab6a6b8u, 0xde5ebe39u, 0xf38ff732u, 0x8989b138u,
            0x33f14961u, 0xc01937bdu, 0xf506c6dau, 0xe4625e7eu, 0xa308ea99u, 0x4e23e33cu, 0x79cbd7ccu, 0x48a14367u,
            0xa3149619u, 0xfec94bd5u, 0xa114174au, 0xeaa01866u, 0xa084db2du, 0x09a8486fu, 0xa888614au, 0x2900af98u,
            0x01665991u, 0xe1992863u, 0xc8f30c60u, 0x2e78ef3cu, 0xd0d51932u, 0xcf0fec14u, 0xf7ca07d2u, 0xd0a82072u,
            0xfd41197eu, 0x9305a6b0u, 0xe86be3dau, 0x74bed3cdu, 0x372da53cu, 0x4c7f4448u, 0xdab5d440u, 0x6dba0ec3u,
            0x083919a7u, 0x9fbaeed9u, 0x49dbcfb0u, 0x4e670c53u, 0x5c3d9c01u, 0x64bdb941u, 0x2c0e636au, 0xba7dd9cdu,
            0xea6f7388u, 0xe70bc762u, 0x35f29adbu, 0x5c4cdd8du, 0xf0d48d8cu, 0xb88153e2u, 0x08a19866u, 0x1ae2eac8u,
            0x284caf89u, 0xaa928223u, 0x9334be53u, 0x3b3a21bfu, 0x16434be3u, 0x9aea3906u, 0xefe8c36eu, 0xf890cdd9u,
            0x80226daeu, 0xc340a4a3u, 0xdf7e9c09u, 0xa694a807u, 0x5b7c5eccu, 0x221db3a6u, 0x9a69a02fu, 0x68818a54u,
            0xceb2296fu, 0x53c0843au, 0xfe893655u, 0x25bfe68au, 0xb4628abcu, 0xcf222ebfu, 0x25ac6f48u, 0xa9a99387u,
            0x53bddb65u, 0xe76ffbe7u, 0xe967fd78u, 0x0ba93563u, 0x8e342bc1u, 0xe8a11be9u, 0x4980740du, 0xc8087dfcu,
            0x8de4bf99u, 0xa11101a0u, 0x7fd37975u, 0xda5a26c0u, 0xe81f994fu, 0x9528cd89u, 0xfd339fedu, 0xb87834bfu,
            0x5f04456du, 0x22258698u, 0xc9c4c83bu, 0x2dc156beu, 0x4f628daau, 0x57f55ec5u, 0xe2220abeu, 0xd2916ebfu,
            0x4ec75b95u, 0x24f2c3c0u, 0x42d15d99u, 0xcd0d7fa0u, 0x7b6e27ffu, 0xa8dc8af0u, 0x7345c106u, 0xf41e232fu,
            0x35162386u, 0xe6ea8926u, 0x3333b094u, 0x157ec6f2u, 0x372b74afu, 0x692573e4u, 0xe9a9d848u, 0xf3160289u,
            0x3a62ef1du, 0xa787e238u, 0xf3a5f676u, 0x74364853u, 0x20951063u, 0x4576698du, 0xb6fad407u, 0x592af950u,
            0x36f73523u, 0x4cfb6e87u, 0x7da4cec0u, 0x6c152daau, 0xcb0396a8u, 0xc50dfe5du, 0xfcd707abu, 0x0921c42fu,
            0x89dff0bbu, 0x5fe2be78u, 0x448f4f33u, 0x754613c9u, 0x2b05d08du, 0x48b9d585u, 0xdc049441u, 0xc8098f9bu,
            0x7dede786u, 0xc39a3373u, 0x42410005u, 0x6a091751u, 0x0ef3c8a6u, 0x890072d6u, 0x28207682u, 0xa9a9f7beu,
            0xbf32679du, 0xd45b5b75u, 0xb353fd00u, 0xcbb0e358u, 0x830f220au, 0x1f8fb214u, 0xd372cf08u, 0xcc3c4a13u,
            0x8cf63166u, 0x061c87beu, 0x88c98f88u, 0x6062e397u, 0x47cf8e7au, 0xb6c85283u, 0x3cc2acfbu, 0x3fc06976u,
            0x4e8f0252u, 0x64d8314du, 0xda3870e3u, 0x1e665459u, 0xc10908f0u, 0x513021a5u, 0x6c5b68b7u, 0x822f8aa0u,
            0x3007cd3eu, 0x74719eefu, 0xdc872681u, 0x073340d4u, 0x7e432fd9u, 0x0c5ec241u, 0x8809286cu, 0xf592d891u,
            0x08a930f6u, 0x957ef305u, 0xb7fbffbdu, 0xc266e96fu, 0x6fe4ac98u, 0xb173ecc0u, 0xbc60b42au, 0x953498dau,
            0xfba1ae12u, 0x2d4bd736u, 0x0f25faabu, 0xa4f3fcebu, 0xe2969123u, 0x257f0c3du, 0x9348af49u, 0x361400bcu,
            0xe8816f4au, 0x3814f200u, 0xa3f94043u, 0x9c7a54c2u, 0xbc704f57u, 0xda41e7f9u, 0xc25ad33au, 0x54f4a084u,
            0xb17f5505u, 0x59357cbeu, 0xedbd15c8u, 0x7f97c5abu, 0xba5ac7b5u, 0xb6f6deafu, 0x3a479c3au, 0x5302da25u,
            0x653d7e6au, 0x54268d49u, 0x51a477eau, 0x5017d55bu, 0xd7d25d88u, 0x44136c76u, 0x0404a8c8u, 0xb8e5a121u,
            0xb81a928au, 0x60ed5869u, 0x97c55b96u, 0xeaec991bu, 0x29935913u, 0x01fdb7f1u, 0x088e8dfau, 0x9ab6f6f5u,
            0x3b4cbf9fu, 0x4a5de3abu, 0xe6051d35u, 0xa0e1d855u, 0xd36b4cf1u, 0xf544edebu, 0xb0e93524u, 0xbebb8fbdu,
            0xa2d762cfu, 0x49c92f54u, 0x38b5f331u, 0x7128a454u, 0x48392905u, 0xa65b1db8u, 0x851c97bdu, 0xd675cf2fu
        };

        private static readonly uint[] S7_SBox =
        {
            0x85e04019u, 0x332bf567u, 0x662dbfffu, 0xcfc65693u, 0x2a8d7f6fu, 0xab9bc912u, 0xde6008a1u, 0x2028da1fu,
            0x0227bce7u, 0x4d642916u, 0x18fac300u, 0x50f18b82u, 0x2cb2cb11u, 0xb232e75cu, 0x4b3695f2u, 0xb28707deu,
            0xa05fbcf6u, 0xcd4181e9u, 0xe150210cu, 0xe24ef1bdu, 0xb168c381u, 0xfde4e789u, 0x5c79b0d8u, 0x1e8bfd43u,
            0x4d495001u, 0x38be4341u, 0x913cee1du, 0x92a79c3fu, 0x089766beu, 0xbaeeadf4u, 0x1286becfu, 0xb6eacb19u,
            0x2660c200u, 0x7565bde4u, 0x64241f7au, 0x8248dca9u, 0xc3b3ad66u, 0x28136086u, 0x0bd8dfa8u, 0x356d1cf2u,
            0x107789beu, 0xb3b2e9ceu, 0x0502aa8fu, 0x0bc0351eu, 0x166bf52au, 0xeb12ff82u, 0xe3486911u, 0xd34d7516u,
            0x4e7b3affu, 0x5f43671bu, 0x9cf6e037u, 0x4981ac83u, 0x334266ceu, 0x8c9341b7u, 0xd0d854c0u, 0xcb3a6c88u,
            0x47bc2829u, 0x4725ba37u, 0xa66ad22bu, 0x7ad61f1eu, 0x0c5cbafau, 0x4437f107u, 0xb6e79962u, 0x42d2d816u,
            0x0a961288u, 0xe1a5c06eu, 0x13749e67u, 0x72fc081au, 0xb1d139f7u, 0xf9583745u, 0xcf19df58u, 0xbec3f756u,
            0xc06eba30u, 0x07211b24u, 0x45c28829u, 0xc95e317fu, 0xbc8ec511u, 0x38bc46e9u, 0xc6e6fa14u, 0xbae8584au,
            0xad4ebc46u, 0x468f508bu, 0x7829435fu, 0xf124183bu, 0x821dba9fu, 0xaff60ff4u, 0xea2c4e6du, 0x16e39264u,
            0x92544a8bu, 0x009b4fc3u, 0xaba68cedu, 0x9ac96f78u, 0x06a5b79au, 0xb2856e6eu, 0x1aec3ca9u, 0xbe838688u,
            0x0e0804e9u, 0x55f1be56u, 0xe7e5363bu, 0xb3a1f25du, 0xf7debb85u, 0x61fe033cu, 0x16746233u, 0x3c034c28u,
            0xda6d0c74u, 0x79aac56cu, 0x3ce4e1adu, 0x51f0c802u, 0x98f8f35au, 0x1626a49fu, 0xeed82b29u, 0x1d382fe3u,
            0x0c4fb99au, 0xbb325778u, 0x3ec6d97bu, 0x6e77a6a9u, 0xcb658b5cu, 0xd45230c7u, 0x2bd1408bu, 0x60c03eb7u,
            0xb9068d78u, 0xa33754f4u, 0xf430c87du, 0xc8a71302u, 0xb96d8c32u, 0xebd4e7beu, 0xbe8b9d2du, 0x7979fb06u,
            0xe7225308u, 0x8b75cf77u, 0x11ef8da4u, 0xe083c858u, 0x8d6b786fu, 0x5a6317a6u, 0xfa5cf7a0u, 0x5dda0033u,
            0xf28ebfb0u, 0xf5b9c310u, 0xa0eac280u, 0x08b9767au, 0xa3d9d2b0u, 0x79d34217u, 0x021a718du, 0x9ac6336au,
            0x2711fd60u, 0x438050e3u, 0x069908a8u, 0x3d7fedc4u, 0x826d2befu, 0x4eeb8476u, 0x488dcf25u, 0x36c9d566u,
            0x28e74e41u, 0xc2610acau, 0x3d49a9cfu, 0xbae3b9dfu, 0xb65f8de6u, 0x92aeaf64u, 0x3ac7d5e6u, 0x9ea80509u,
            0xf22b017du, 0xa4173f70u, 0xdd1e16c3u, 0x15e0d7f9u, 0x50b1b887u, 0x2b9f4fd5u, 0x625aba82u, 0x6a017962u,
            0x2ec01b9cu, 0x15488aa9u, 0xd716e740u, 0x40055a2cu, 0x93d29a22u, 0xe32dbf9au, 0x058745b9u, 0x3453dc1eu,
            0xd699296eu, 0x496cff6fu, 0x1c9f4986u, 0xdfe2ed07u, 0xb87242d1u, 0x19de7eaeu, 0x053e561au, 0x15ad6f8cu,
            0x66626c1cu, 0x7154c24cu, 0xea082b2au, 0x93eb2939u, 0x17dcb0f0u, 0x58d4f2aeu, 0x9ea294fbu, 0x52cf564cu,
            0x9883fe66u, 0x2ec40581u, 0x763953c3u, 0x01d6692eu, 0xd3a0c108u, 0xa1e7160eu, 0xe4f2dfa6u, 0x693ed285u,
            0x74904698u, 0x4c2b0eddu, 0x4f757656u, 0x5d393378u, 0xa132234fu, 0x3d321c5du, 0xc3f5e194u, 0x4b269301u,
            0xc79f022fu, 0x3c997e7eu, 0x5e4f9504u, 0x3ffafbbdu, 0x76f7ad0eu, 0x296693f4u, 0x3d1fce6fu, 0xc61e45beu,
            0xd3b5ab34u, 0xf72bf9b7u, 0x1b0434c0u, 0x4e72b567u, 0x5592a33du, 0xb5229301u, 0xcfd2a87fu, 0x60aeb767u,
            0x1814386bu, 0x30bcc33du, 0x38a0c07du, 0xfd1606f2u, 0xc363519bu, 0x589dd390u, 0x5479f8e6u, 0x1cb8d647u,
            0x97fd61a9u, 0xea7759f4u, 0x2d57539du, 0x569a58cfu, 0xe84e63adu, 0x462e1b78u, 0x6580f87eu, 0xf3817914u,
            0x91da55f4u, 0x40a230f3u, 0xd1988f35u, 0xb6e318d2u, 0x3ffa50bcu, 0x3d40f021u, 0xc3c0bdaeu, 0x4958c24cu,
            0x518f36b2u, 0x84b1d370u, 0x0fedce83u, 0x878ddadau, 0xf2a279c7u, 0x94e01be8u, 0x90716f4bu, 0x954b8aa3u
        };

        private static readonly uint[] S8_SBox =
        {
            0xe216300du, 0xbbddfffcu, 0xa7ebdabdu, 0x35648095u, 0x7789f8b7u, 0xe6c1121bu, 0x0e241600u, 0x052ce8b5u,
            0x11a9cfb0u, 0xe5952f11u, 0xece7990au, 0x9386d174u, 0x2a42931cu, 0x76e38111u, 0xb12def3au, 0x37ddddfcu,
            0xde9adeb1u, 0x0a0cc32cu, 0xbe197029u, 0x84a00940u, 0xbb243a0fu, 0xb4d137cfu, 0xb44e79f0u, 0x049eedfdu,
            0x0b15a15du, 0x480d3168u, 0x8bbbde5au, 0x669ded42u, 0xc7ece831u, 0x3f8f95e7u, 0x72df191bu, 0x7580330du,
            0x94074251u, 0x5c7dcdfau, 0xabbe6d63u, 0xaa402164u, 0xb301d40au, 0x02e7d1cau, 0x53571daeu, 0x7a3182a2u,
            0x12a8ddecu, 0xfdaa335du, 0x176f43e8u, 0x71fb46d4u, 0x38129022u, 0xce949ad4u, 0xb84769adu, 0x965bd862u,
            0x82f3d055u, 0x66fb9767u, 0x15b80b4eu, 0x1d5b47a0u, 0x4cfde06fu, 0xc28ec4b8u, 0x57e8726eu, 0x647a78fcu,
            0x99865d44u, 0x608bd593u, 0x6c200e03u, 0x39dc5ff6u, 0x5d0b00a3u, 0xae63aff2u, 0x7e8bd632u, 0x70108c0cu,
            0xbbd35049u, 0x2998df04u, 0x980cf42au, 0x9b6df491u, 0x9e7edd53u, 0x06918548u, 0x58cb7e07u, 0x3b74ef2eu,
            0x522fffb1u, 0xd24708ccu, 0x1c7e27cdu, 0xa4eb215bu, 0x3cf1d2e2u, 0x19b47a38u, 0x424f7618u, 0x35856039u,
            0x9d17dee7u, 0x27eb35e6u, 0xc9aff67bu, 0x36baf5b8u, 0x09c467cdu, 0xc18910b1u, 0xe11dbf7bu, 0x06cd1af8u,
            0x7170c608u, 0x2d5e3354u, 0xd4de495au, 0x64c6d006u, 0xbcc0c62cu, 0x3dd00db3u, 0x708f8f34u, 0x77d51b42u,
            0x264f620fu, 0x24b8d2bfu, 0x15c1b79eu, 0x46a52564u, 0xf8d7e54eu, 0x3e378160u, 0x7895cda5u, 0x859c15a5u,
            0xe6459788u, 0xc37bc75fu, 0xdb07ba0cu, 0x0676a3abu, 0x7f229b1eu, 0x31842e7bu, 0x24259fd7u, 0xf8bef472u,
            0x835ffcb8u, 0x6df4c1f2u, 0x96f5b195u, 0xfd0af0fcu, 0xb0fe134cu, 0xe2506d3du, 0x4f9b12eau, 0xf215f225u,
            0xa223736fu, 0x9fb4c428u, 0x25d04979u, 0x34c713f8u, 0xc4618187u, 0xea7a6e98u, 0x7cd16efcu, 0x1436876cu,
            0xf1544107u, 0xbedeee14u, 0x56e9af27u, 0xa04aa441u, 0x3cf7c899u, 0x92ecbae6u, 0xdd67016du, 0x151682ebu,
            0xa842eedfu, 0xfdba60b4u, 0xf1907b75u, 0x20e3030fu, 0x24d8c29eu, 0xe139673bu, 0xefa63fb8u, 0x71873054u,
            0xb6f2cf3bu, 0x9f326442u, 0xcb15a4ccu, 0xb01a4504u, 0xf1e47d8du, 0x844a1be5u, 0xbae7dfdcu, 0x42cbda70u,
            0xcd7dae0au, 0x57e85b7au, 0xd53f5af6u, 0x20cf4d8cu, 0xcea4d428u, 0x79d130a4u, 0x3486ebfbu, 0x33d3cddcu,
            0x77853b53u, 0x37effcb5u, 0xc5068778u, 0xe580b3e6u, 0x4e68b8f4u, 0xc5c8b37eu, 0x0d809ea2u, 0x398feb7cu,
            0x132a4f94u, 0x43b7950eu, 0x2fee7d1cu, 0x223613bdu, 0xdd06caa2u, 0x37df932bu, 0xc4248289u, 0xacf3ebc3u,
            0x5715f6b7u, 0xef3478ddu, 0xf267616fu, 0xc148cbe4u, 0x9052815eu, 0x5e410fabu, 0xb48a2465u, 0x2eda7fa4u,
            0xe87b40e4u, 0xe98ea084u, 0x5889e9e1u, 0xefd390fcu, 0xdd07d35bu, 0xdb485694u, 0x38d7e5b2u, 0x57720101u,
            0x730edebcu, 0x5b643113u, 0x94917e4fu, 0x503c2fbau, 0x646f1282u, 0x7523d24au, 0xe0779695u, 0xf9c17a8fu,
            0x7a5b2121u, 0xd187b896u, 0x29263a4du, 0xba510cdfu, 0x81f47c9fu, 0xad1163edu, 0xea7b5965u, 0x1a00726eu,
            0x11403092u, 0x00da6d77u, 0x4a0cdd61u, 0xad1f4603u, 0x605bdfb0u, 0x9eedc364u, 0x22ebe6a8u, 0xcee7d28au,
            0xa0e736a0u, 0x5564a6b9u, 0x10853209u, 0xc7eb8f37u, 0x2de705cau, 0x8951570fu, 0xdf09822bu, 0xbd691a6cu,
            0xaa12e4f2u, 0x87451c0fu, 0xe0f6a27au, 0x3ada4819u, 0x4cf1764fu, 0x0d771c2bu, 0x67cdb156u, 0x350d8384u,
            0x5938fa0fu, 0x42399ef3u, 0x36997b07u, 0x0e84093du, 0x4aa93e61u, 0x8360d87bu, 0x1fa98b0cu, 0x1149382cu,
            0xe97625a5u, 0x0614d1b7u, 0x0e25244bu, 0x0c768347u, 0x589e8d82u, 0x0d2059d1u, 0xa466bb1eu, 0xf8da0a82u,
            0x04f19130u, 0xba6e4ec0u, 0x99265164u, 0x1ee7230du, 0x50b2ad80u, 0xeaee6801u, 0x8db2a283u, 0xea8bf59eu
        };

        #endregion

        #region S1 - S4 S-Boxes

        private static readonly uint[] S1_SBox =
        {
            0x30fb40d4u, 0x9fa0ff0bu, 0x6beccd2fu, 0x3f258c7au, 0x1e213f2fu, 0x9c004dd3u, 0x6003e540u, 0xcf9fc949u,
            0xbfd4af27u, 0x88bbbdb5u, 0xe2034090u, 0x98d09675u, 0x6e63a0e0u, 0x15c361d2u, 0xc2e7661du, 0x22d4ff8eu,
            0x28683b6fu, 0xc07fd059u, 0xff2379c8u, 0x775f50e2u, 0x43c340d3u, 0xdf2f8656u, 0x887ca41au, 0xa2d2bd2du,
            0xa1c9e0d6u, 0x346c4819u, 0x61b76d87u, 0x22540f2fu, 0x2abe32e1u, 0xaa54166bu, 0x22568e3au, 0xa2d341d0u,
            0x66db40c8u, 0xa784392fu, 0x004dff2fu, 0x2db9d2deu, 0x97943facu, 0x4a97c1d8u, 0x527644b7u, 0xb5f437a7u,
            0xb82cbaefu, 0xd751d159u, 0x6ff7f0edu, 0x5a097a1fu, 0x827b68d0u, 0x90ecf52eu, 0x22b0c054u, 0xbc8e5935u,
            0x4b6d2f7fu, 0x50bb64a2u, 0xd2664910u, 0xbee5812du, 0xb7332290u, 0xe93b159fu, 0xb48ee411u, 0x4bff345du,
            0xfd45c240u, 0xad31973fu, 0xc4f6d02eu, 0x55fc8165u, 0xd5b1caadu, 0xa1ac2daeu, 0xa2d4b76du, 0xc19b0c50u,
            0x882240f2u, 0x0c6e4f38u, 0xa4e4bfd7u, 0x4f5ba272u, 0x564c1d2fu, 0xc59c5319u, 0xb949e354u, 0xb04669feu,
            0xb1b6ab8au, 0xc71358ddu, 0x6385c545u, 0x110f935du, 0x57538ad5u, 0x6a390493u, 0xe63d37e0u, 0x2a54f6b3u,
            0x3a787d5fu, 0x6276a0b5u, 0x19a6fcdfu, 0x7a42206au, 0x29f9d4d5u, 0xf61b1891u, 0xbb72275eu, 0xaa508167u,
            0x38901091u, 0xc6b505ebu, 0x84c7cb8cu, 0x2ad75a0fu, 0x874a1427u, 0xa2d1936bu, 0x2ad286afu, 0xaa56d291u,
            0xd7894360u, 0x425c750du, 0x93b39e26u, 0x187184c9u, 0x6c00b32du, 0x73e2bb14u, 0xa0bebc3cu, 0x54623779u,
            0x64459eabu, 0x3f328b82u, 0x7718cf82u, 0x59a2cea6u, 0x04ee002eu, 0x89fe78e6u, 0x3fab0950u, 0x325ff6c2u,
            0x81383f05u, 0x6963c5c8u, 0x76cb5ad6u, 0xd49974c9u, 0xca180dcfu, 0x380782d5u, 0xc7fa5cf6u, 0x8ac31511u,
            0x35e79e13u, 0x47da91d0u, 0xf40f9086u, 0xa7e2419eu, 0x31366241u, 0x051ef495u, 0xaa573b04u, 0x4a805d8du,
            0x548300d0u, 0x00322a3cu, 0xbf64cddfu, 0xba57a68eu, 0x75c6372bu, 0x50afd341u, 0xa7c13275u, 0x915a0bf5u,
            0x6b54bfabu, 0x2b0b1426u, 0xab4cc9d7u, 0x449ccd82u, 0xf7fbf265u, 0xab85c5f3u, 0x1b55db94u, 0xaad4e324u,
            0xcfa4bd3fu, 0x2deaa3e2u, 0x9e204d02u, 0xc8bd25acu, 0xeadf55b3u, 0xd5bd9e98u, 0xe31231b2u, 0x2ad5ad6cu,
            0x954329deu, 0xadbe4528u, 0xd8710f69u, 0xaa51c90fu, 0xaa786bf6u, 0x22513f1eu, 0xaa51a79bu, 0x2ad344ccu,
            0x7b5a41f0u, 0xd37cfbadu, 0x1b069505u, 0x41ece491u, 0xb4c332e6u, 0x032268d4u, 0xc9600accu, 0xce387e6du,
            0xbf6bb16cu, 0x6a70fb78u, 0x0d03d9c9u, 0xd4df39deu, 0xe01063dau, 0x4736f464u, 0x5ad328d8u, 0xb347cc96u,
            0x75bb0fc3u, 0x98511bfbu, 0x4ffbcc35u, 0xb58bcf6au, 0xe11f0abcu, 0xbfc5fe4au, 0xa70aec10u, 0xac39570au,
            0x3f04442fu, 0x6188b153u, 0xe0397a2eu, 0x5727cb79u, 0x9ceb418fu, 0x1cacd68du, 0x2ad37c96u, 0x0175cb9du,
            0xc69dff09u, 0xc75b65f0u, 0xd9db40d8u, 0xec0e7779u, 0x4744ead4u, 0xb11c3274u, 0xdd24cb9eu, 0x7e1c54bdu,
            0xf01144f9u, 0xd2240eb1u, 0x9675b3fdu, 0xa3ac3755u, 0xd47c27afu, 0x51c85f4du, 0x56907596u, 0xa5bb15e6u,
            0x580304f0u, 0xca042cf1u, 0x011a37eau, 0x8dbfaadbu, 0x35ba3e4au, 0x3526ffa0u, 0xc37b4d09u, 0xbc306ed9u,
            0x98a52666u, 0x5648f725u, 0xff5e569du, 0x0ced63d0u, 0x7c63b2cfu, 0x700b45e1u, 0xd5ea50f1u, 0x85a92872u,
            0xaf1fbda7u, 0xd4234870u, 0xa7870bf3u, 0x2d3b4d79u, 0x42e04198u, 0x0cd0ede7u, 0x26470db8u, 0xf881814cu,
            0x474d6ad7u, 0x7c0c5e5cu, 0xd1231959u, 0x381b7298u, 0xf5d2f4dbu, 0xab838653u, 0x6e2f1e23u, 0x83719c9eu,
            0xbd91e046u, 0x9a56456eu, 0xdc39200cu, 0x20c8c571u, 0x962bda1cu, 0xe1e696ffu, 0xb141ab08u, 0x7cca89b9u,
            0x1a69e783u, 0x02cc4843u, 0xa2f7c579u, 0x429ef47du, 0x427b169cu, 0x5ac9f049u, 0xdd8f0f00u, 0x5c8165bfu
        };

        private static readonly uint[] S2_SBox =
        {
            0x1f201094u, 0xef0ba75bu, 0x69e3cf7eu, 0x393f4380u, 0xfe61cf7au, 0xeec5207au, 0x55889c94u, 0x72fc0651u,
            0xada7ef79u, 0x4e1d7235u, 0xd55a63ceu, 0xde0436bau, 0x99c430efu, 0x5f0c0794u, 0x18dcdb7du, 0xa1d6eff3u,
            0xa0b52f7bu, 0x59e83605u, 0xee15b094u, 0xe9ffd909u, 0xdc440086u, 0xef944459u, 0xba83ccb3u, 0xe0c3cdfbu,
            0xd1da4181u, 0x3b092ab1u, 0xf997f1c1u, 0xa5e6cf7bu, 0x01420ddbu, 0xe4e7ef5bu, 0x25a1ff41u, 0xe180f806u,
            0x1fc41080u, 0x179bee7au, 0xd37ac6a9u, 0xfe5830a4u, 0x98de8b7fu, 0x77e83f4eu, 0x79929269u, 0x24fa9f7bu,
            0xe113c85bu, 0xacc40083u, 0xd7503525u, 0xf7ea615fu, 0x62143154u, 0x0d554b63u, 0x5d681121u, 0xc866c359u,
            0x3d63cf73u, 0xcee234c0u, 0xd4d87e87u, 0x5c672b21u, 0x071f6181u, 0x39f7627fu, 0x361e3084u, 0xe4eb573bu,
            0x602f64a4u, 0xd63acd9cu, 0x1bbc4635u, 0x9e81032du, 0x2701f50cu, 0x99847ab4u, 0xa0e3df79u, 0xba6cf38cu,
            0x10843094u, 0x2537a95eu, 0xf46f6ffeu, 0xa1ff3b1fu, 0x208cfb6au, 0x8f458c74u, 0xd9e0a227u, 0x4ec73a34u,
            0xfc884f69u, 0x3e4de8dfu, 0xef0e0088u, 0x3559648du, 0x8a45388cu, 0x1d804366u, 0x721d9bfdu, 0xa58684bbu,
            0xe8256333u, 0x844e8212u, 0x128d8098u, 0xfed33fb4u, 0xce280ae1u, 0x27e19ba5u, 0xd5a6c252u, 0xe49754bdu,
            0xc5d655ddu, 0xeb667064u, 0x77840b4du, 0xa1b6a801u, 0x84db26a9u, 0xe0b56714u, 0x21f043b7u, 0xe5d05860u,
            0x54f03084u, 0x066ff472u, 0xa31aa153u, 0xdadc4755u, 0xb5625dbfu, 0x68561be6u, 0x83ca6b94u, 0x2d6ed23bu,
            0xeccf01dbu, 0xa6d3d0bau, 0xb6803d5cu, 0xaf77a709u, 0x33b4a34cu, 0x397bc8d6u, 0x5ee22b95u, 0x5f0e5304u,
            0x81ed6f61u, 0x20e74364u, 0xb45e1378u, 0xde18639bu, 0x881ca122u, 0xb96726d1u, 0x8049a7e8u, 0x22b7da7bu,
            0x5e552d25u, 0x5272d237u, 0x79d2951cu, 0xc60d894cu, 0x488cb402u, 0x1ba4fe5bu, 0xa4b09f6bu, 0x1ca815cfu,
            0xa20c3005u, 0x8871df63u, 0xb9de2fcbu, 0x0cc6c9e9u, 0x0beeff53u, 0xe3214517u, 0xb4542835u, 0x9f63293cu,
            0xee41e729u, 0x6e1d2d7cu, 0x50045286u, 0x1e6685f3u, 0xf33401c6u, 0x30a22c95u, 0x31a70850u, 0x60930f13u,
            0x73f98417u, 0xa1269859u, 0xec645c44u, 0x52c877a9u, 0xcdff33a6u, 0xa02b1741u, 0x7cbad9a2u, 0x2180036fu,
            0x50d99c08u, 0xcb3f4861u, 0xc26bd765u, 0x64a3f6abu, 0x80342676u, 0x25a75e7bu, 0xe4e6d1fcu, 0x20c710e6u,
            0xcdf0b680u, 0x17844d3bu, 0x31eef84du, 0x7e0824e4u, 0x2ccb49ebu, 0x846a3baeu, 0x8ff77888u, 0xee5d60f6u,
            0x7af75673u, 0x2fdd5cdbu, 0xa11631c1u, 0x30f66f43u, 0xb3faec54u, 0x157fd7fau, 0xef8579ccu, 0xd152de58u,
            0xdb2ffd5eu, 0x8f32ce19u, 0x306af97au, 0x02f03ef8u, 0x99319ad5u, 0xc242fa0fu, 0xa7e3ebb0u, 0xc68e4906u,
            0xb8da230cu, 0x80823028u, 0xdcdef3c8u, 0xd35fb171u, 0x088a1bc8u, 0xbec0c560u, 0x61a3c9e8u, 0xbca8f54du,
            0xc72feffau, 0x22822e99u, 0x82c570b4u, 0xd8d94e89u, 0x8b1c34bcu, 0x301e16e6u, 0x273be979u, 0xb0ffeaa6u,
            0x61d9b8c6u, 0x00b24869u, 0xb7ffce3fu, 0x08dc283bu, 0x43daf65au, 0xf7e19798u, 0x7619b72fu, 0x8f1c9ba4u,
            0xdc8637a0u, 0x16a7d3b1u, 0x9fc393b7u, 0xa7136eebu, 0xc6bcc63eu, 0x1a513742u, 0xef6828bcu, 0x520365d6u,
            0x2d6a77abu, 0x3527ed4bu, 0x821fd216u, 0x095c6e2eu, 0xdb92f2fbu, 0x5eea29cbu, 0x145892f5u, 0x91584f7fu,
            0x5483697bu, 0x2667a8ccu, 0x85196048u, 0x8c4baceau, 0x833860d4u, 0x0d23e0f9u, 0x6c387e8au, 0x0ae6d249u,
            0xb284600cu, 0xd835731du, 0xdcb1c647u, 0xac4c56eau, 0x3ebd81b3u, 0x230eabb0u, 0x6438bc87u, 0xf0b5b1fau,
            0x8f5ea2b3u, 0xfc184642u, 0x0a036b7au, 0x4fb089bdu, 0x649da589u, 0xa345415eu, 0x5c038323u, 0x3e5d3bb9u,
            0x43d79572u, 0x7e6dd07cu, 0x06dfdf1eu, 0x6c6cc4efu, 0x7160a539u, 0x73bfbe70u, 0x83877605u, 0x4523ecf1u
        };

        private static readonly uint[] S3_SBox =
        {
            0x8defc240u, 0x25fa5d9fu, 0xeb903dbfu, 0xe810c907u, 0x47607fffu, 0x369fe44bu, 0x8c1fc644u, 0xaececa90u,
            0xbeb1f9bfu, 0xeefbcaeau, 0xe8cf1950u, 0x51df07aeu, 0x920e8806u, 0xf0ad0548u, 0xe13c8d83u, 0x927010d5u,
            0x11107d9fu, 0x07647db9u, 0xb2e3e4d4u, 0x3d4f285eu, 0xb9afa820u, 0xfade82e0u, 0xa067268bu, 0x8272792eu,
            0x553fb2c0u, 0x489ae22bu, 0xd4ef9794u, 0x125e3fbcu, 0x21fffceeu, 0x825b1bfdu, 0x9255c5edu, 0x1257a240u,
            0x4e1a8302u, 0xbae07fffu, 0x528246e7u, 0x8e57140eu, 0x3373f7bfu, 0x8c9f8188u, 0xa6fc4ee8u, 0xc982b5a5u,
            0xa8c01db7u, 0x579fc264u, 0x67094f31u, 0xf2bd3f5fu, 0x40fff7c1u, 0x1fb78dfcu, 0x8e6bd2c1u, 0x437be59bu,
            0x99b03dbfu, 0xb5dbc64bu, 0x638dc0e6u, 0x55819d99u, 0xa197c81cu, 0x4a012d6eu, 0xc5884a28u, 0xccc36f71u,
            0xb843c213u, 0x6c0743f1u, 0x8309893cu, 0x0feddd5fu, 0x2f7fe850u, 0xd7c07f7eu, 0x02507fbfu, 0x5afb9a04u,
            0xa747d2d0u, 0x1651192eu, 0xaf70bf3eu, 0x58c31380u, 0x5f98302eu, 0x727cc3c4u, 0x0a0fb402u, 0x0f7fef82u,
            0x8c96fdadu, 0x5d2c2aaeu, 0x8ee99a49u, 0x50da88b8u, 0x8427f4a0u, 0x1eac5790u, 0x796fb449u, 0x8252dc15u,
            0xefbd7d9bu, 0xa672597du, 0xada840d8u, 0x45f54504u, 0xfa5d7403u, 0xe83ec305u, 0x4f91751au, 0x925669c2u,
            0x23efe941u, 0xa903f12eu, 0x60270df2u, 0x0276e4b6u, 0x94fd6574u, 0x927985b2u, 0x8276dbcbu, 0x02778176u,
            0xf8af918du, 0x4e48f79eu, 0x8f616ddfu, 0xe29d840eu, 0x842f7d83u, 0x340ce5c8u, 0x96bbb682u, 0x93b4b148u,
            0xef303cabu, 0x984faf28u, 0x779faf9bu, 0x92dc560du, 0x224d1e20u, 0x8437aa88u, 0x7d29dc96u, 0x2756d3dcu,
            0x8b907ceeu, 0xb51fd240u, 0xe7c07ce3u, 0xe566b4a1u, 0xc3e9615eu, 0x3cf8209du, 0x6094d1e3u, 0xcd9ca341u,
            0x5c76460eu, 0x00ea983bu, 0xd4d67881u, 0xfd47572cu, 0xf76cedd9u, 0xbda8229cu, 0x127dadaau, 0x438a074eu,
            0x1f97c090u, 0x081bdb8au, 0x93a07ebeu, 0xb938ca15u, 0x97b03cffu, 0x3dc2c0f8u, 0x8d1ab2ecu, 0x64380e51u,
            0x68cc7bfbu, 0xd90f2788u, 0x12490181u, 0x5de5ffd4u, 0xdd7ef86au, 0x76a2e214u, 0xb9a40368u, 0x925d958fu,
            0x4b39fffau, 0xba39aee9u, 0xa4ffd30bu, 0xfaf7933bu, 0x6d498623u, 0x193cbcfau, 0x27627545u, 0x825cf47au,
            0x61bd8ba0u, 0xd11e42d1u, 0xcead04f4u, 0x127ea392u, 0x10428db7u, 0x8272a972u, 0x9270c4a8u, 0x127de50bu,
            0x285ba1c8u, 0x3c62f44fu, 0x35c0eaa5u, 0xe805d231u, 0x428929fbu, 0xb4fcdf82u, 0x4fb66a53u, 0x0e7dc15bu,
            0x1f081fabu, 0x108618aeu, 0xfcfd086du, 0xf9ff2889u, 0x694bcc11u, 0x236a5caeu, 0x12deca4du, 0x2c3f8cc5u,
            0xd2d02dfeu, 0xf8ef5896u, 0xe4cf52dau, 0x95155b67u, 0x494a488cu, 0xb9b6a80cu, 0x5c8f82bcu, 0x89d36b45u,
            0x3a609437u, 0xec00c9a9u, 0x44715253u, 0x0a874b49u, 0xd773bc40u, 0x7c34671cu, 0x02717ef6u, 0x4feb5536u,
            0xa2d02fffu, 0xd2bf60c4u, 0xd43f03c0u, 0x50b4ef6du, 0x07478cd1u, 0x006e1888u, 0xa2e53f55u, 0xb9e6d4bcu,
            0xa2048016u, 0x97573833u, 0xd7207d67u, 0xde0f8f3du, 0x72f87b33u, 0xabcc4f33u, 0x7688c55du, 0x7b00a6b0u,
            0x947b0001u, 0x570075d2u, 0xf9bb88f8u, 0x8942019eu, 0x4264a5ffu, 0x856302e0u, 0x72dbd92bu, 0xee971b69u,
            0x6ea22fdeu, 0x5f08ae2bu, 0xaf7a616du, 0xe5c98767u, 0xcf1febd2u, 0x61efc8c2u, 0xf1ac2571u, 0xcc8239c2u,
            0x67214cb8u, 0xb1e583d1u, 0xb7dc3e62u, 0x7f10bdceu, 0xf90a5c38u, 0x0ff0443du, 0x606e6dc6u, 0x60543a49u,
            0x5727c148u, 0x2be98a1du, 0x8ab41738u, 0x20e1be24u, 0xaf96da0fu, 0x68458425u, 0x99833be5u, 0x600d457du,
            0x282f9350u, 0x8334b362u, 0xd91d1120u, 0x2b6d8da0u, 0x642b1e31u, 0x9c305a00u, 0x52bce688u, 0x1b03588au,
            0xf7baefd5u, 0x4142ed9cu, 0xa4315c11u, 0x83323ec5u, 0xdfef4636u, 0xa133c501u, 0xe9d3531cu, 0xee353783u
        };

        private static readonly uint[] S4_SBox =
        {
            0x9db30420u, 0x1fb6e9deu, 0xa7be7befu, 0xd273a298u, 0x4a4f7bdbu, 0x64ad8c57u, 0x85510443u, 0xfa020ed1u,
            0x7e287affu, 0xe60fb663u, 0x095f35a1u, 0x79ebf120u, 0xfd059d43u, 0x6497b7b1u, 0xf3641f63u, 0x241e4adfu,
            0x28147f5fu, 0x4fa2b8cdu, 0xc9430040u, 0x0cc32220u, 0xfdd30b30u, 0xc0a5374fu, 0x1d2d00d9u, 0x24147b15u,
            0xee4d111au, 0x0fca5167u, 0x71ff904cu, 0x2d195ffeu, 0x1a05645fu, 0x0c13fefeu, 0x081b08cau, 0x05170121u,
            0x80530100u, 0xe83e5efeu, 0xac9af4f8u, 0x7fe72701u, 0xd2b8ee5fu, 0x06df4261u, 0xbb9e9b8au, 0x7293ea25u,
            0xce84ffdfu, 0xf5718801u, 0x3dd64b04u, 0xa26f263bu, 0x7ed48400u, 0x547eebe6u, 0x446d4ca0u, 0x6cf3d6f5u,
            0x2649abdfu, 0xaea0c7f5u, 0x36338cc1u, 0x503f7e93u, 0xd3772061u, 0x11b638e1u, 0x72500e03u, 0xf80eb2bbu,
            0xabe0502eu, 0xec8d77deu, 0x57971e81u, 0xe14f6746u, 0xc9335400u, 0x6920318fu, 0x081dbb99u, 0xffc304a5u,
            0x4d351805u, 0x7f3d5ce3u, 0xa6c866c6u, 0x5d5bcca9u, 0xdaec6feau, 0x9f926f91u, 0x9f46222fu, 0x3991467du,
            0xa5bf6d8eu, 0x1143c44fu, 0x43958302u, 0xd0214eebu, 0x022083b8u, 0x3fb6180cu, 0x18f8931eu, 0x281658e6u,
            0x26486e3eu, 0x8bd78a70u, 0x7477e4c1u, 0xb506e07cu, 0xf32d0a25u, 0x79098b02u, 0xe4eabb81u, 0x28123b23u,
            0x69dead38u, 0x1574ca16u, 0xdf871b62u, 0x211c40b7u, 0xa51a9ef9u, 0x0014377bu, 0x041e8ac8u, 0x09114003u,
            0xbd59e4d2u, 0xe3d156d5u, 0x4fe876d5u, 0x2f91a340u, 0x557be8deu, 0x00eae4a7u, 0x0ce5c2ecu, 0x4db4bba6u,
            0xe756bdffu, 0xdd3369acu, 0xec17b035u, 0x06572327u, 0x99afc8b0u, 0x56c8c391u, 0x6b65811cu, 0x5e146119u,
            0x6e85cb75u, 0xbe07c002u, 0xc2325577u, 0x893ff4ecu, 0x5bbfc92du, 0xd0ec3b25u, 0xb7801ab7u, 0x8d6d3b24u,
            0x20c763efu, 0xc366a5fcu, 0x9c382880u, 0x0ace3205u, 0xaac9548au, 0xeca1d7c7u, 0x041afa32u, 0x1d16625au,
            0x6701902cu, 0x9b757a54u, 0x31d477f7u, 0x9126b031u, 0x36cc6fdbu, 0xc70b8b46u, 0xd9e66a48u, 0x56e55a79u,
            0x026a4cebu, 0x52437effu, 0x2f8f76b4u, 0x0df980a5u, 0x8674cde3u, 0xedda04ebu, 0x17a9be04u, 0x2c18f4dfu,
            0xb7747f9du, 0xab2af7b4u, 0xefc34d20u, 0x2e096b7cu, 0x1741a254u, 0xe5b6a035u, 0x213d42f6u, 0x2c1c7c26u,
            0x61c2f50fu, 0x6552daf9u, 0xd2c231f8u, 0x25130f69u, 0xd8167fa2u, 0x0418f2c8u, 0x001a96a6u, 0x0d1526abu,
            0x63315c21u, 0x5e0a72ecu, 0x49bafefdu, 0x187908d9u, 0x8d0dbd86u, 0x311170a7u, 0x3e9b640cu, 0xcc3e10d7u,
            0xd5cad3b6u, 0x0caec388u, 0xf73001e1u, 0x6c728affu, 0x71eae2a1u, 0x1f9af36eu, 0xcfcbd12fu, 0xc1de8417u,
            0xac07be6bu, 0xcb44a1d8u, 0x8b9b0f56u, 0x013988c3u, 0xb1c52fcau, 0xb4be31cdu, 0xd8782806u, 0x12a3a4e2u,
            0x6f7de532u, 0x58fd7eb6u, 0xd01ee900u, 0x24adffc2u, 0xf4990fc5u, 0x9711aac5u, 0x001d7b95u, 0x82e5e7d2u,
            0x109873f6u, 0x00613096u, 0xc32d9521u, 0xada121ffu, 0x29908415u, 0x7fbb977fu, 0xaf9eb3dbu, 0x29c9ed2au,
            0x5ce2a465u, 0xa730f32cu, 0xd0aa3fe8u, 0x8a5cc091u, 0xd49e2ce7u, 0x0ce454a9u, 0xd60acd86u, 0x015f1919u,
            0x77079103u, 0xdea03af6u, 0x78a8565eu, 0xdee356dfu, 0x21f05cbeu, 0x8b75e387u, 0xb3c50651u, 0xb8a5c3efu,
            0xd8eeb6d2u, 0xe523be77u, 0xc2154529u, 0x2f69efdfu, 0xafe67afbu, 0xf470c4b2u, 0xf3e0eb5bu, 0xd6cc9876u,
            0x39e4460cu, 0x1fda8538u, 0x1987832fu, 0xca007367u, 0xa99144f8u, 0x296b299eu, 0x492fc295u, 0x9266beabu,
            0xb5676e69u, 0x9bd3dddau, 0xdf7e052fu, 0xdb25701cu, 0x1b5e51eeu, 0xf65324e6u, 0x6afce36cu, 0x0316cc04u,
            0x8644213eu, 0xb7dc59d0u, 0x7965291fu, 0xccd6fd43u, 0x41823979u, 0x932bcdf6u, 0xb657c34du, 0x4edfd282u,
            0x7ae5290cu, 0x3cb9536bu, 0x851e20feu, 0x9833557eu, 0x13ecf0b0u, 0xd3ffb372u, 0x3f85c5c1u, 0x0aef7ed2u
        };

        #endregion
    }

    /// <summary>
    ///     Implementation of the CAST-128 algorithm as a standard component for
    ///     the .NET security framework.
    ///     In cryptography, CAST5 (alternatively CAST-128) is a block cipher used in a number of products,
    ///     notably as the default cipher in some versions of GPG and PGP. It has also been approved for
    ///     Canadian government use by the Communications Security Establishment. The algorithm was created
    ///     in 1996 by Carlisle Adams and Stafford Tavares using the CAST design procedure; another member
    ///     of the CAST family of ciphers, CAST-256 (a former AES candidate) was derived from CAST-128.
    ///     According to some sources, the "CAST" name is based on the initials of its inventors, though
    ///     Bruce Schneier reports the authors' claim that "the name should conjure up images of randomness" (Schneier, 1996).
    ///     CAST-128 is a 12- or 16-round Feistel network with a 64-bit block size and a key
    ///     size of between 40 to 128 bits (but only in 8-bit increments). The full 16 rounds are used when
    ///     the key size is longer than 80 bits. Components include large 8×32-bit S-boxes based on bent
    ///     functions, key-dependent rotations, modular addition and subtraction, and XOR operations.
    ///     There are three alternating types of round function, but they are similar in structure and
    ///     differ only in the choice of the exact operation (addition, subtraction or XOR) at various points.
    ///     Although Entrust holds a patent on the CAST design procedure, CAST-128 is available worldwide
    ///     on a royalty-free basis for commercial and non-commercial uses.
    /// </summary>
    public class Cast5Managed : SymmetricAlgorithm, ICryptoTransform
    {
        /// <summary>The block size in bytes.</summary>
        public const int BLOCK_SIZE = 8;

        /// <summary>
        ///     The key size for this algorithm is 128 bits (16 BYTES)
        /// </summary>
        public const int MAX_KEY_LENGTH = 16;

        private readonly bool isCBC;

        private readonly bool isEncryption;
        private byte[] lastBlock;
        private readonly int[] scheduledKey;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Cast5Managed" /> class.
        /// </summary>
        /// <exception cref="T:System.Security.Cryptography.CryptographicException">
        ///     The implementation of the class derived from the symmetric algorithm is not valid.
        /// </exception>
        public Cast5Managed()
        {
            this.KeySizeValue = MAX_KEY_LENGTH << 3;

            this.LegalBlockSizesValue = new KeySizes[1];
            this.LegalBlockSizesValue[0] = new KeySizes(BLOCK_SIZE*8, BLOCK_SIZE*8, BLOCK_SIZE);

            this.LegalKeySizesValue = new KeySizes[1];
            this.LegalKeySizesValue[0] = new KeySizes(0, MAX_KEY_LENGTH << 3, 8);
        }

        private Cast5Managed(byte[] key, bool useCBC, bool isEncryptor)
        {
            // Initialize SymmetricAlgorithm fields
            this.Key = key;
            if (useCBC)
            {
                this.ModeValue = CipherMode.CBC;
            }
            else
            {
                this.ModeValue = CipherMode.ECB;
            }

            // Initialize other fields
            this.isCBC = useCBC;
            this.isEncryption = isEncryptor;
            this.scheduledKey = Cast5.ScheduleKey(key);
            this.CreateNewIV();

            this.Padding = PaddingMode.PKCS7;
            //this.Padding = PaddingMode.ANSIX923;
        }

        /// <summary>
        ///     Releases unmanaged resources and performs other cleanup operations before the
        ///     <see cref="Cast5Managed" /> is reclaimed by garbage collection.
        /// </summary>
        ~Cast5Managed()
        {
            // Finalizer calls Dispose(false)
            this.Dispose(false);
        }

        private void CreateNewIV()
        {
            long iv = Cast5.GetOrdinaryIV(this.scheduledKey);
            this.SetIV(iv);
        }

        private void SetIV(long iv)
        {
            this.IV = BitConverter.GetBytes(iv);
        }

        private long GetIV()
        {
            return BitConverter.ToInt64(this.IV, 0);
        }

        /// <summary>
        ///     Performs last block padding checks
        /// </summary>
        /// <returns>
        ///     The number of byte to return after having de-padded the last block
        ///     A <see cref="System.Int32" />
        /// </returns>
        private int CheckLastBlockPadding(byte[] buffer)
        {
            int rest = -1;
            byte bytesToFill; // The byte value used for padding

            if (PaddingMode.PKCS7 == this.PaddingValue)
            {
                bytesToFill = buffer[buffer.Length - 1];
                if (BLOCK_SIZE < bytesToFill)
                {
                    throw new CryptographicException("PKCS7 Error: Bytes to fill is longer than CAST128 Block Size");
                }
                rest = buffer.Length - bytesToFill;
                for (int i = buffer.Length - 2; i >= rest; i--)
                {
                    if (buffer[i] != bytesToFill)
                    {
                        throw new CryptographicException("PKCS7 Error: Trailing bytes are not coherent.");
                    }
                }
            }
            else if (PaddingMode.ANSIX923 == this.PaddingValue)
            {
                bytesToFill = buffer[buffer.Length - 1];
                if (BLOCK_SIZE < bytesToFill)
                {
                    throw new CryptographicException("ANSIX923 Error: Bytes to fill is longer than CAST128 Block Size");
                }
                rest = buffer.Length - bytesToFill;
                for (int i = buffer.Length - 2; i >= rest; i--)
                {
                    if (buffer[i] != 0)
                    {
                        throw new CryptographicException("ANSIX923 Error: Trailing bytes are not zero");
                    }
                }
            }

            return rest;
        }

        #region SymmetricAlgorithm Members

        /// <summary>
        ///     Creates a symmetric decryptor object with the specified
        ///     <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> property and initialization vector (
        ///     <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).
        /// </summary>
        /// <param name="rgbKey">The secret key to use for the symmetric algorithm.</param>
        /// <param name="rgbIV">The initialization vector to use for the symmetric algorithm.</param>
        /// <returns>A symmetric decryptor object.</returns>
        public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
        {
            return new Cast5Managed(rgbKey, true, false);
        }

        /// <summary>
        ///     Creates a symmetric encryptor object with the specified
        ///     <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" /> property and initialization vector (
        ///     <see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />).
        /// </summary>
        /// <param name="rgbKey">The secret key to use for the symmetric algorithm.</param>
        /// <param name="rgbIV">The initialization vector to use for the symmetric algorithm.</param>
        /// <returns>A symmetric encryptor object.</returns>
        public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
        {
            return new Cast5Managed(rgbKey, true, true);
        }

        /// <summary>
        ///     Generates a random initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) to
        ///     use for the algorithm.
        /// </summary>
        public override void GenerateIV()
        {
            this.CreateNewIV();
        }

        /// <summary>
        ///     Generates a random key (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.Key" />) to use for the
        ///     algorithm.
        /// </summary>
        public override void GenerateKey()
        {
            Guid newg = Guid.NewGuid();
            this.Key = newg.ToByteArray();
        }

        /// <summary>
        ///     Gets or sets the secret key for the symmetric algorithm.
        /// </summary>
        /// <value></value>
        /// <see cref="System.Security.Cryptography.SymmetricAlgorithm.Key" />
        public override byte[] Key
        {
            get { return this.KeyValue; }
            set { this.KeyValue = value; }
        }

        /// <summary>
        ///     Gets or sets the size, in bits, of the secret key used by the symmetric algorithm.
        /// </summary>
        /// <value></value>
        /// <see cref="System.Security.Cryptography.SymmetricAlgorithm.KeySize" />
        public override int KeySize
        {
            get { return this.KeySizeValue; }
            set
            {
                KeySizes ks = this.LegalKeySizes[0];

                if ((0 != value%ks.SkipSize) ||
                    (value > ks.MaxSize) ||
                    (value < ks.MinSize))
                {
                    throw new CryptographicException("Invalid key size");
                }
                this.KeySizeValue = value;
            }
        }

        /// <summary>
        ///     Gets or sets the initialization vector (<see cref="P:System.Security.Cryptography.SymmetricAlgorithm.IV" />) for
        ///     the symmetric algorithm.
        /// </summary>
        /// <value></value>
        /// <see cref="System.Security.Cryptography.SymmetricAlgorithm.IV" />
        public override byte[] IV
        {
            get
            {
                if (null == this.IVValue)
                {
                    this.GenerateIV();
                }
                return (byte[]) this.IVValue.Clone();
            }
            set
            {
                if (null == value)
                {
                    throw new ArgumentNullException("IV");
                }
                if (value.Length != BLOCK_SIZE)
                {
                    throw new CryptographicException("Invalid IV length: get " + value.Length + ", expected " +
                                                     BLOCK_SIZE + " bytes.");
                }
                this.IVValue = (byte[]) value.Clone();
            }
        }

        /// <summary>
        ///     Gets or sets the mode for operation of the symmetric algorithm.
        /// </summary>
        /// <value></value>
        /// <see cref="System.Security.Cryptography.SymmetricAlgorithm.Mode" />
        public override CipherMode Mode
        {
            get { return this.ModeValue; }
            set
            {
                if ((value != CipherMode.CBC) &&
                    (value != CipherMode.ECB))
                {
                    throw new CryptographicException("Invalid cipher mode");
                }
                this.ModeValue = value;
            }
        }

        #endregion

        #region ICryptoTransform Members

        /// <summary>
        ///     Gets a value indicating whether the current transform can be reused.
        /// </summary>
        /// <value></value>
        /// <returns>
        ///     true if the current transform can be reused; otherwise, false.
        /// </returns>
        public bool CanReuseTransform
        {
            get { return true; }
        }

        /// <summary>
        ///     Gets a value indicating whether multiple blocks can be transformed.
        /// </summary>
        /// <value></value>
        /// <returns>
        ///     true if multiple blocks can be transformed; otherwise, false.
        /// </returns>
        public bool CanTransformMultipleBlocks
        {
            get { return true; }
        }

        /// <summary>
        ///     Gets the input block size.
        /// </summary>
        /// <value></value>
        /// <returns>
        ///     The size of the input data blocks in bytes.
        /// </returns>
        public int InputBlockSize
        {
            get { return BLOCK_SIZE; }
        }

        /// <summary>
        ///     Gets the output block size.
        /// </summary>
        /// <value></value>
        /// <returns>
        ///     The size of the output data blocks in bytes.
        /// </returns>
        public int OutputBlockSize
        {
            get { return BLOCK_SIZE; }
        }

        /// <summary>
        ///     Transforms the specified region of the input byte array and copies the resulting transform to the specified region
        ///     of the output byte array.
        /// </summary>
        /// <param name="inputBuffer">The input for which to compute the transform.</param>
        /// <param name="inputOffset">The offset into the input byte array from which to begin using data.</param>
        /// <param name="inputCount">The number of bytes in the input byte array to use as data.</param>
        /// <param name="outputBuffer">The output to which to write the transform.</param>
        /// <param name="outputOffset">The offset into the output byte array from which to begin writing data.</param>
        /// <returns>The number of bytes written.</returns>
        public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer,
            int outputOffset)
        {
            if (0 == inputCount)
            {
                return 0;
            }

            if (0 != inputCount%BLOCK_SIZE)
            {
                throw new CryptographicException("Unexpected unaligned data");
            }

            long newIv = 0;
            int processedBytes = inputCount;
            byte[] operationsBuffer = new byte[inputCount];

            // Copy the input array to a temporary workable array (operationsBuffer)
            Array.Copy(inputBuffer, operationsBuffer, inputCount);

            if (this.isEncryption)
            {
                if (this.isCBC)
                {
                    newIv = Cast5.EncryptCBC(this.scheduledKey, operationsBuffer, inputOffset, inputCount, this.GetIV());
                    this.SetIV(newIv);
                }
                else
                {
                    Cast5.EncryptECB(this.scheduledKey, operationsBuffer, inputOffset);
                }

                Array.Copy(operationsBuffer, outputBuffer, processedBytes);
            }
            else // Decryption
            {
                const int blockSize = BLOCK_SIZE;
                int offset = 0;

                // Prepend the last block to the output
                if (null == this.lastBlock)
                {
                    this.lastBlock = new byte[blockSize];
                }
                else
                {
                    Array.Copy(this.lastBlock, 0, outputBuffer, outputOffset, blockSize);
                    outputOffset += blockSize;
                    offset += blockSize;
                }

                if (this.isCBC)
                {
                    newIv = Cast5.DecryptCBC(this.scheduledKey, operationsBuffer, inputOffset, inputCount, this.GetIV());
                    this.SetIV(newIv);
                }
                else
                {
                    Cast5.DecryptECB(this.scheduledKey, operationsBuffer, inputOffset);
                }

                // Saves the operations' buffer (8 bytes) as the last block
                Array.Copy(operationsBuffer, inputCount - blockSize, this.lastBlock, 0, blockSize);


                // First time the processed bytes are buffer.Len - block size
                if (offset == 0)
                {
                    processedBytes = operationsBuffer.Length - blockSize;
                }

                inputCount -= blockSize;

                Array.Copy(operationsBuffer, 0, outputBuffer, offset, operationsBuffer.Length - blockSize);
            }

            return processedBytes;
        }

        /// <summary>
        ///     Transforms the specified region of the specified byte array.
        /// </summary>
        /// <param name="inputBuffer">The input for which to compute the transform.</param>
        /// <param name="inputOffset">The offset into the byte array from which to begin using data.</param>
        /// <param name="inputCount">The number of bytes in the byte array to use as data.</param>
        /// <returns>The computed transform.</returns>
        public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
        {
            if (inputCount > BLOCK_SIZE)
            {
                throw new CryptographicException("Input count greater than block size");
            }

            byte[] outputBuffer = new byte[BLOCK_SIZE];
            byte[] buffer = new byte[BLOCK_SIZE]; // Initializes buffer with zeroes

            // Copies the first inputCount bytes to the temporary array
            for (int j = 0; j < inputCount; j++)
            {
                buffer[j] = inputBuffer[j];
            }

            if (this.isEncryption)
            {
                int outputBufferLen = BLOCK_SIZE +
                                      (BLOCK_SIZE == inputCount ? BLOCK_SIZE : 0);
                outputBuffer = new byte[outputBufferLen];
                buffer = new byte[outputBufferLen];

                // Padding adjustments
                if (this.Padding == PaddingMode.PKCS7)
                {
                    // The number of bytes to fill and the byte that will be put into
                    byte bytesToFill = (byte) (BLOCK_SIZE - inputCount);

                    for (int i = inputCount; i < outputBufferLen; i++)
                    {
                        buffer[i] = bytesToFill;
                    }
                }
                else if (this.Padding == PaddingMode.ANSIX923)
                {
                    for (int i = inputCount; i < outputBufferLen - 1; i++)
                    {
                        buffer[i] = 0;
                    }
                    buffer[outputBufferLen - 1] = (byte) (outputBufferLen - inputCount);
                }
                else
                {
                    throw new CryptographicException("Unsupported PADDING mode");
                }

                Array.Copy(inputBuffer, inputOffset, buffer, 0, inputCount);

                if (this.isCBC)
                {
                    Cast5.EncryptCBC(this.scheduledKey, buffer, inputOffset, BLOCK_SIZE, this.GetIV());
                }
                else
                {
                    Cast5.EncryptECB(this.scheduledKey, buffer, inputOffset);
                }

                Array.Copy(buffer, outputBuffer, BLOCK_SIZE);
            }
            else // Decryption
            {
                if (0 == inputCount)
                {
                    // Normally, inputCount should be zero. We copy the block in memory
                    // to the output block
                    Array.Copy(this.lastBlock, buffer, BLOCK_SIZE);
                }
                else
                {
                    if (this.isCBC)
                    {
                        Cast5.DecryptCBC(this.scheduledKey, buffer, inputOffset, BLOCK_SIZE, this.GetIV());
                    }
                    else
                    {
                        Cast5.DecryptECB(this.scheduledKey, buffer, inputOffset);
                    }
                }

                int rest = this.CheckLastBlockPadding(buffer);
                outputBuffer = new byte[inputCount + rest];

                if (inputCount > 0)
                {
                    Array.Copy(this.lastBlock, 0, outputBuffer, 0, this.lastBlock.Length);
                    Array.Copy(buffer, 0, outputBuffer, BLOCK_SIZE, rest);
                }
                else
                {
                    Array.Copy(buffer, outputBuffer, rest);
                }
            }

            return outputBuffer;
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public new void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Releases the unmanaged resources used by the <see cref="T:System.Security.Cryptography.SymmetricAlgorithm" /> and
        ///     optionally releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     true to release both managed and unmanaged resources; false to release only unmanaged
        ///     resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                Cast5.ClearKey(this.scheduledKey);
                this.lastBlock = null;
            }
        }

        #endregion
    }
}