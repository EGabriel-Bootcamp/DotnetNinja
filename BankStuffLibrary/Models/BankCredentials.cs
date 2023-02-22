﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankStuffLibrary.Models
{
    public class BankCredentials
    {
        public string Number { get;  }
        public string Owner { get; set; }
        public string? Notes { get; set; }

        #region BalanceComputation
        public decimal Balance
        {
            get
            {
                decimal balance = 0;
                foreach (var item in allTransactions)
                {
                    balance += item.Amount;

                }

                return balance;
            }

        }
        #endregion

        private static int accountNumberSeed = 1234567890;

        public BankCredentials(string name, decimal initialBalance, string note)
        {
            Owner = name;
            //Balance= initialBalance;
            MakeDeposit(initialBalance, DateTime.Now, note); //,"Initial balance");
            Number = accountNumberSeed.ToString();
            accountNumberSeed++;
            Notes = note;
        }

        private List<Transaction> allTransactions = new List<Transaction>();

        public void MakeDeposit(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
            }
            var deposit = new Transaction(amount, date, note);
            allTransactions.Add(deposit);
        }

        public void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (amount <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
            }
            if (Balance - amount < 0)
            {
                throw new InvalidOperationException("Not sufficient funds for this withdrawal");
            }
            var withdrawal = new Transaction(-amount, date, note);
            allTransactions.Add(withdrawal);
        }

        public string GetAccountHistory()
        {
            var report = new System.Text.StringBuilder();

            decimal balance = 0;
            report.AppendLine("Deposit Date\tDeposit Amount\tCurrent Balance In Account");
            foreach (var item in allTransactions)
            {
                balance += item.Amount;
                //report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
                report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t\t\t{balance}");
            }

            return report.ToString();
        }
    }

}