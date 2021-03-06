﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassField
{
    /// <summary>
    /// Таблица результатов
    /// </summary>
    public class TableOfResult
    {
        /// <summary>
        /// Название соревнований
        /// </summary>
        public string Competitions { get; set; }
        /// <summary>
        /// Название категории соревнований
        /// </summary>
        public string CategoryCompetition { get; set; }
        /// <summary>
        /// Возрастная категория
        /// </summary>
        public string AgeСategory { get; set; }
        /// <summary>
        /// Список участников
        /// </summary>
        public List<Competitor> ListCompetitors { get; set; }
        /// <summary>
        /// Дата проведения
        /// </summary>
        public DateTime DateCompetition { get; set; }
        /// <summary>
        /// Название блоков жюри, которые учавствовали в оценивании
        /// </summary>
        public List<Blockjury> NameJury { get; set; }
        /// <summary>
        /// Город проведения
        /// </summary>
        public string CityofComp { get; set; }
    }
    /// <summary>
    /// Жюри
    /// </summary>
    public class Blockjury
    {
        /// <summary>
        /// Имена членов жюри
        /// </summary>
        public List<string> ListJury { get; set; }
        /// <summary>
        /// Название блока Жюри
        /// </summary>
        public string NameJury { get; set; }
        /// <summary>
        /// Фотография Главного члена жюри
        /// </summary>
        public byte[] Photo { get; set; }
    }

    public class Competitor
    {
        /// <summary>
        /// Имя участника
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Категория оценивания
        /// </summary>
        public CategoryScore CategoryScore { get; set; }
        /// <summary>
        /// Оценка за категорию
        /// </summary>
        public double Score1 { get; set; }
        public double Score2 { get; set; }
        /// <summary>
        /// Общий балл нескольких категорий
        /// </summary>
        public double AllScore { get; set; }
        /// <summary>
        /// Фотография участника
        /// </summary>
        public byte[] Photo { get; set; }
        /// <summary>
        /// Название блоков жюри, которые учавствовали в оценивании по определенным категориям
        /// </summary>
        public string NameJury { get; set; }
    }
    /// <summary>
    /// Категории оценивания
    /// </summary>
    public enum CategoryScore
    {
        /// <summary>
        /// Артистичность
        /// </summary>
        Artistry,
        /// <summary>
        /// Техничность
        /// </summary>
        Technique,
        /// <summary>
        /// Сложность
        /// </summary>
        Difficulty,
    }
}