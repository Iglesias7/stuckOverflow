using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
namespace Prid1920_g03.Models {

    public class Tag {
        [Key]
       public int Id {get; set;} 
       public string Name {get; set;}

        public virtual IList<PostTag> PostTags { get; set; } = new List<PostTag>();

        [NotMapped]
        public string Body {get {
            var tag = "";
            
              if(Name == "angular"){
                  tag = "Questions about Angular (not to be confused with AngularJS),the web framework from Google. Use this tag for Angular questions which are not specific to an individual version. For the older AngularJS (1.x) web framework, use the angularjs tag.";
              }
              else if(Name == "typescript"){
                  tag = "TypeScript is a statically typed superset of JavaScript created by Microsoft that adds optional types, classes, interfaces, enums, generics, async/await, and many other features, and compiles to plain JavaScript. This tag is for questions specific to TypeScript. It is not used for general JavaScript questions.";
              }

              else if(Name == "csharp" ){
                  tag = "C# (pronounced 'see sharp') is a high level, statically typed, multi-paradigm programming language developed by Microsoft. C# code usually targets Microsoft's .NET family of tools and run-times, which include the .NET Framework and .NET Core. Use this tag for questions about code written in C# or C#'s formal specification.";
              }
              else if(Name == "EntityFramework Core"){
                  tag = "Entity Framework (EF) Core is an open source ORM developed by Microsoft.";
              }
              else if(Name == "dotnet core" ){
                  tag = ".NET Core is an open-source successor of the .NET Framework. It can be used in a wide variety of applications and verticals, ranging from servers and data centers to apps and devices. .NET Core is supported by Microsoft on Windows, Linux and macOS";
              }
              else if(Name ==  "mysql"){
                  tag = "MySQL is a free, open source Relational Database Management System (RDBMS) that uses Structured Query Language (SQL). DO NOT USE this tag for other DBs such as SQL Server, SQLite etc. Those are different DBs which all use their own dialects of SQL to manage the data.";
              }
              else{
                  tag = Name+" is an programming language or framework.If that not the case then look for it on google.";
              }
            
            return tag;

        }
        }

        [NotMapped]
        public int NbXPosts {
            get{             
                return PostTags.Count();
            }
        }

        [NotMapped]
        public DateTime Timestamp {get
        {
            var date = new DateTime();
            if(Name == "angular"){
                date = new DateTime(2019,4,18,9,0,0);
            }
            else if(Name == "typescript"){
                date = new DateTime(2019,7,28,15,35,30);
            }
            else if(Name == "csharp"){
                date = new DateTime(2019,9,20,11,45,20);

            }
            else if(Name == "EntityFramework Core"){
                date = new DateTime(2019,5,15,19,55,11);
            }
            else if(Name == "dotnet core"){
                date = new DateTime(2019,11,30,6,0,0);
            }
            else if(Name ==  "mysql"){
                date = new DateTime(2009,12,18,1,52,0);
            }
            else {
                date = DateTime.Now;
            }
            return date;
        }
        }
       
    }
}