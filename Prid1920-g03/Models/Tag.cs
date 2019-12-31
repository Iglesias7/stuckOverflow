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
              else if(Name == "javascript"){
                  tag = "JavaScript (not to be confused with Java) is a high-level, dynamic, multi-paradigm, object-oriented, prototype-based, weakly-typed, and interpreted language used for both client-side and server-side scripting. Its primary use is in the rendering and manipulation of web pages. Use this tag for questions regarding ECMAScript and its various dialects/implementations (excluding ActionScript and Google-Apps-Script).";
              }
              else if(Name == "java"){
                  tag = "Java (not to be confused with JavaScript, JScript or JS) is a general-purpose, platform-independent, statically typed, object-oriented programming language designed to be used in conjunction with the Java Virtual Machine (JVM). 'Java platform' is the name for a computing system that has installed tools for developing and running Java programs. Use this tag for questions referring to the Java programming language or Java platform tools";
              }
              else if(Name == "php"){
                  tag = "PHP (PHP: Hypertext Preprocessor) is a widely used, high-level, dynamic, object-oriented and interpreted scripting language primarily designed for server-side web development.";
              }
              else if(Name == "python"){
                  tag = "Python is a multi-paradigm, dynamically typed, multipurpose programming language, designed to be quick (to learn, to use, and to understand), and to enforce a clean and uniform syntax. Two similar but incompatible versions of Python are commonly in use, Python 2.7 and 3.x. For version-specific Python questions, add the [python-2.7] or [python-3.x] tag. When using a Python variant or library (e.g. Jython, PyPy, Pandas, Numpy), please include it in the tags.";
              }
              else if(Name == "android"){
                  tag = "Android is Google's mobile operating system, used for programming or developing digital devices (Smartphones, Tablets, Automobiles, TVs, Wear, Glass, IoT). For topics related to Android, use Android-specific tags such as android-intent, android-activity, android-adapter, etc. For questions other than development or programming, but related to the Android framework, use this link: https://android.stackexchange.com. ";
              }
              else if(Name == "jquery"){
                  tag = "jQuery is a Javascript library, consider also adding the Javascript tag. jQuery is a popular cross-browser JavaScript library that facilitates Document Object Model (DOM) traversal, event handling, animations and AJAX interactions by minimizing the discrepancies across browsers. A question tagged jquery should be related to jquery, so jquery should be used by the code in question and at least jquery usage-related elements need to be in the question. ";
              }
              else if(Name == "html"){
                  tag = "HTML (Hypertext Markup Language) is the standard markup language used for structuring web pages and formatting content. HTML describes the structure of a website semantically along with cues for presentation, making it a markup language, rather than a programming language. HTML works in conjunction primarily with CSS and JavaScript, adding style and behavior to the pages. The most recent revision to the HTML specification is HTML5.2.";
              }
              else if(Name == "c++"){
                  tag = "C++ is a general-purpose programming language. It was originally designed as an extension to C, and has a similar syntax, but is now a completely different language. Use this tag for questions about code (to be) compiled with a C++ compiler. Use a version specific tag for questions related to a specific standard revision [C++11], [C++14], [C++17] or [C++20] etc.";
              }
              else if(Name == "ios"){
                  tag = "iOS is the mobile operating system running on the Apple iPhone, iPod touch, and iPad. Use this tag [ios] for questions related to programming on the iOS platform. Use the related tags [objective-c] and [swift] for issues specific to those programming languages. ";
              }
              else if(Name == "css"){
                  tag = "CSS (Cascading Style Sheets) is a representation style sheet language used for describing the look and formatting of HTML (HyperText Markup Language), XML (Extensible Markup Language) documents and SVG elements including (but not limited to) colours, layout, fonts, and animations. It also describes how elements should be rendered on screen, on paper, in speech, or on other media. ";
              }
              else if(Name == "reactjs"){
                  tag = "React (also known as React.js or ReactJS) is a JavaScript library for building user interfaces. It uses a declarative paradigm and aims to be both efficient and flexible.";
              }
              else if(Name == "wordpress"){
                  tag = "This tag is for programming-specific questions about the WordPress content management system. Off-topic questions include those about theme development, WordPress administration, management best practices, server configuration, etc. These are best asked on Stack Exchange WordPress Development. ";
              }
              else if(Name =="windows"){
                  tag = "Writing software specific to the Microsoft Windows operating system: APIs, behaviors, etc. Note: GENERAL WINDOWS SUPPORT IS OFF-TOPIC. Support questions may be asked on https://superuser.com";
              }
              else if(Name == "spring"){
                  tag = "The Spring Framework is an open source framework for application development on the Java platform. At its core is rich support for component-based architectures, and it currently has over twenty highly integrated modules.";
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
                date = new DateTime(2019,4,18);
            }
            else if(Name == "typescript"){
                date = new DateTime(1994,7,28);
            }
            else if(Name == "csharp"){
                date = new DateTime(1993,9,20);

            }
            else if(Name == "EntityFramework Core"){
                date = new DateTime(1995,5,15);
            }
            else if(Name == "dotnet core"){
                date = new DateTime(1996,11,30);
            }
            else if(Name ==  "mysql"){
                date = new DateTime(2010,12,18);
            }
            else if(Name ==  "javascript"){
                date = new DateTime(2011,1,8);
            }
            else if(Name ==  "java"){
                date = new DateTime(2009,2,15);
            }
             else if(Name ==  "php"){
                date = new DateTime(2003,3,10);
            }
            else if(Name ==  "python"){
                date = new DateTime(2016,4,13);
            }
            else if(Name ==  "android"){
                date = new DateTime(2015,5,29);
            }
            else if(Name ==  "jquery"){
                date = new DateTime(2014,8,29);
            }
            else if(Name ==  "html"){
                date = new DateTime(2001,11,29);
            }
            else if(Name ==  "c++"){
                date = new DateTime(2004,9,23);
            }
             else if(Name ==  "ios"){
                date = new DateTime(1998,9,5);
            }
            else if(Name ==  "css"){
                date = new DateTime(2000,12,15);
            }
            else if(Name ==  "reactjs"){
                date = new DateTime(2018,10,25);
            }
            else if(Name ==  "wordpress"){
                date = new DateTime(1990,3,15);
            }
            else if(Name ==  "windows"){
                date = new DateTime(2015,12,29);
            }
            else if(Name ==  "spring"){
                date = new DateTime(2014,1,16);
            }
            else {
                date = DateTime.Now;
            }
            return date;
        }
        }
       
    }
}