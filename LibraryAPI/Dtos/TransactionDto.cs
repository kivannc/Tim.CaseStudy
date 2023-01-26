﻿using LibraryAPI.Models;

namespace LibraryAPI.Dtos
{
    public class TransactionDto
    {
        public BookDto Book { get; set; }

        public MemberDto Member { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime BorrowDate { get; set; }

        public int LateDays => (int)(DateTime.Now - DueDate).TotalDays;

        public double Penalty { get; set; }

        public BookStatus BookStatus { get; set; }


    }
}