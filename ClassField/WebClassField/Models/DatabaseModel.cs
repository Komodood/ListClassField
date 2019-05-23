using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace WebClassField.Models
{
    namespace DataAccessPostgreSqlProvider
    {
        public class WebClassFielddbContext : DbContext
        {
            // >dotnet ef migration add testMigration in AspNet5MultipleProject
            public WebClassFielddbContext()
            {
                Database.EnsureCreated();
            }

            public WebClassFielddbContext(DbContextOptions<WebClassFielddbContext> options) : base(options)
            {
            }

            public DbSet<DbTableOfResult> TableOfResult { get; set; }

            public static string ConnectionString { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseNpgsql(WebClassFielddbContext.ConnectionString);

                base.OnConfiguring(optionsBuilder);
            }
        }

        public class DbTableOfResult
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

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
            public List<DbCompetitor> ListCompetitors { get; set; }
            /// <summary>
            /// Дата проведения
            /// </summary>
            public DateTime DateCompetition { get; set; }
            /// <summary>
            /// Название блоков жюри, которые учавствовали в оценивании
            /// </summary>
            public List<DbBlockjury> NameJury { get; set; }
            /// <summary>
            /// Город проведения
            /// </summary>
            public string CityofComp { get; set; }
        }
        /// <summary>
        /// Жюри
        /// </summary>
        public class DbBlockjury
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            public int TableOfResultId { get; set; }
            [ForeignKey("TableOfResultId")]
            public virtual DbTableOfResult TableOfResult { get; set; }

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

        public class DbCompetitor
        {
            [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            public int TableOfResultId { get; set; }
            [ForeignKey("TableOfResultId")]
            public virtual DbTableOfResult TableOfResult { get; set; }
            /// <summary>
            /// Имя участника
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// Категория оценивания
            /// </summary>
            ///public CategoryScore CategoryScore { get; set; }
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

    }
}