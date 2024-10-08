﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPISchoolTest.Domain.AggregateModels.StudentAggregate
{
    public class Student
    {
        [Key] // Đánh dấu là khóa chính
        public Guid StudentUUID { get; set; }

        [Required] // Bắt buộc phải có
        [MaxLength(100)] // Giới hạn độ dài chuỗi
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string Gender { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public double GPA { get; set; }

        [ForeignKey("Class")] // Khóa ngoại liên kết với bảng Classes
        public int ClassID { get; set; }
    }

    public class StudentList
    {
        public Guid StudentID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public double GPA { get; set; }

    }

}
