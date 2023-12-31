﻿namespace DataLayer.Entities;

public class BookAuthors
{
    public int BookId { get; set; }
    public int AuthorId { get; set; }
    public virtual Book Book { get; set; }
    public virtual Author Author { get; set; }
}