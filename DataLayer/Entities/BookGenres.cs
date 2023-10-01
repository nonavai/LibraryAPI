﻿namespace DataLayer.Entities;

public class BookGenres
{
    public int BookId { get; set; }
    public int GenreId { get; set; }
    public virtual Book Book { get; set; }
    public virtual Genre Genre { get; set; }
}