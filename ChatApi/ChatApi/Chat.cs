﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApi
{
    public class Chat
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string? Name1 { get; set; }
        [ForeignKey("User")]
        public string? Name2 { get; set; }

    }
}
