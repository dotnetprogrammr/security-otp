﻿using System;
using System.Globalization;
using Xunit;

namespace Security.Otp.Test
{
    public class TotpGeneratorTest
    {
        private static readonly byte[] sha1RfcSecret = new byte[]
        {
            0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30,
            0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30
        };

        private static readonly byte[] sha256RfcSecret = new byte[]
        {
            0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30,
            0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30,
            0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30,
            0x31, 0x32
        };

        private static readonly byte[] sha512RfcSecret = new byte[]
        {
            0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30,
            0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30,
            0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30,
            0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30,
            0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30,
            0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x30,
            0x31, 0x32, 0x33, 0x34
        };

        [Theory]
        [InlineData("1970-01-01 00:00:59", "94287082")]
        [InlineData("2005-03-18 01:58:29", "07081804")]
        [InlineData("2005-03-18 01:58:31", "14050471")]
        [InlineData("2009-02-13 23:31:30", "89005924")]
        [InlineData("2033-05-18 03:33:20", "69279037")]
        [InlineData("2603-10-11 11:33:20", "65353130")]
        public void OneTimePassword_UsingSHA1_IsGeneratedCorrectlyForRfcTests(string dateTimeString, string expectedPassword)
        {
            // Arrange
            var dateTime = DateTime.ParseExact(dateTimeString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);

            var otpGenerator = new TotpGenerator();

            // Act
            var password = otpGenerator.GeneratePassword(sha1RfcSecret, dateTime, 30, PasswordLengths.EightDigitPassword, Hmac.Sha1);

            // Assert
            Assert.Equal(expectedPassword, password);
        }

        [Theory]
        [InlineData("1970-01-01 00:00:59", "46119246")]
        [InlineData("2005-03-18 01:58:29", "68084774")]
        [InlineData("2005-03-18 01:58:31", "67062674")]
        [InlineData("2009-02-13 23:31:30", "91819424")]
        [InlineData("2033-05-18 03:33:20", "90698825")]
        [InlineData("2603-10-11 11:33:20", "77737706")]
        public void OneTimePassword_UsingSHA256_IsGeneratedCorrectlyForRfcTests(string dateTimeString, string expectedPassword)
        {
            // Arrange
            var dateTime = DateTime.ParseExact(dateTimeString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);

            var otpGenerator = new TotpGenerator();

            // Act
            var password = otpGenerator.GeneratePassword(sha256RfcSecret, dateTime, 30, PasswordLengths.EightDigitPassword, Hmac.Sha256);

            // Assert
            Assert.Equal(expectedPassword, password);
        }

        [Theory]
        [InlineData("1970-01-01 00:00:59", "90693936")]
        [InlineData("2005-03-18 01:58:29", "25091201")]
        [InlineData("2005-03-18 01:58:31", "99943326")]
        [InlineData("2009-02-13 23:31:30", "93441116")]
        [InlineData("2033-05-18 03:33:20", "38618901")]
        [InlineData("2603-10-11 11:33:20", "47863826")]
        public void OneTimePassword_UsingSHA512_IsGeneratedCorrectlyForRfcTests(string dateTimeString, string expectedPassword)
        {
            // Arrange
            var dateTime = DateTime.ParseExact(dateTimeString, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);

            var otpGenerator = new TotpGenerator();

            // Act
            var password = otpGenerator.GeneratePassword(sha512RfcSecret, dateTime, 30, PasswordLengths.EightDigitPassword, Hmac.Sha512);

            // Assert
            Assert.Equal(expectedPassword, password);
        }
    }
}
