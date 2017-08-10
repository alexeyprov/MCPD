//===============================================================================
// Microsoft patterns & practices
// Windows Azure Architecture Guide
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// This code released under the terms of the 
// Microsoft patterns & practices license (http://wag.codeplex.com/license)
//===============================================================================


namespace AExpense.Model
{
    using System;

    [Serializable]
    public class ExpenseKey
    {
        private string invertedTicks;

        public ExpenseKey(string revertedTicks)
        {
            this.InvertedTicks = revertedTicks;
        }

        public static ExpenseKey Now
        {
            get
            {
                return new ExpenseKey(string.Format("{0:D19}", DateTime.MaxValue.Ticks - DateTime.UtcNow.Ticks));
            }
        }

        public string InvertedTicks
        {
            get
            {
                return this.invertedTicks;
            }

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("InvertedTicks cannot be null or empty.");    
                }

                if (value.Length != 19)
                {
                    throw new ArgumentException("The reverted ticks have to be a string of 19 characters. Get it using ExpenseKey.Now.");
                }

                this.invertedTicks = value;
            }
        }

        public static bool operator ==(ExpenseKey left, ExpenseKey right)
        {
            if (((object)left == null) && ((object)right == null))
            {
                return true;
            }

            if (((object)left) == null)
            {
                return false;
            }

            if (((object)right) == null)
            {
                return false;
            }

            return left.invertedTicks == right.invertedTicks;
        }

        public static bool operator !=(ExpenseKey left, ExpenseKey right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return this.invertedTicks;
        }

        public override bool Equals(object obj)
        {
            ExpenseKey otherExpenseKey = obj as ExpenseKey;

            if (otherExpenseKey == null)
            {
                return base.Equals(obj);
            }

            return this.InvertedTicks.Equals(otherExpenseKey.InvertedTicks, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return this.invertedTicks.GetHashCode();
        }
    }
}