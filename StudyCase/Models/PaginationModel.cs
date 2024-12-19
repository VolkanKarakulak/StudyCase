﻿namespace StudyCase.Models
{
    public class PaginationModel
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? Search {  get; set; }
        public int? PageCount { get; set;}
    }
}
