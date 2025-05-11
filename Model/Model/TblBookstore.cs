using System;
using System.Collections.Generic;

namespace Model.Model;

public partial class TblBookstore
{
    public int Id { get; set; }

    public string? BookCategoryId { get; set; }

    public string? BookCategory { get; set; }

    public string? BookName { get; set; }

    public string? Stock{ get; set; }
}
