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
              else if(Name =="validation"){
                tag ="Validation is used to check data to make sure it fits whatever required specifications are set for it. Typically Validation is used in checking input data, and in verifying data before storage.";
              }
              else if(Name =="sockets"){
                  tag ="An endpoint of a bi-directional inter-process communication flow. This often refers to a process flow over a network connection, but by no means is limited to such. Not to be confused with WebSocket (a protocol) or other abstractions (e.g. socket.io). ";
              }
              else if(Name =="apache-spark"){
                  tag ="Apache Spark is an open source distributed data processing engine written in Scala providing a unified API and distributed data sets to users. Use Cases for Apache Spark often are related to machine/deep learning, graph processing. ";
              }
              else if(Name =="http"){
                  tag ="Hypertext Transfer Protocol (HTTP) is an application level network protocol that is used for the transfer of content on the World Wide Web.";
              }
              else if(Name =="xaml"){
                  tag ="Extensible Application Markup Language (XAML) is a declarative XML-based language used for initializing structured values and objects in various frameworks. When a question is about the usage of XAML with a specific framework a tag for the framework should also be provided e.g. [wpf] (Windows Presentation Foundation), [silverlight], [windows-phone], [windows-store-apps] (Windows 8 store apps), [win-universal-app], [xamarin.forms] or [workflow-foundation]";
              }
              else if(Name =="opencv"){
                  tag ="OpenCV (Open Source Computer Vision) is a library for real time computer vision. When using this tag, please mention the OpenCV release you're working with (e.g. 3.4.6), and add a language specific tag (python, c++, ...) if needed.";
              }
              else if(Name =="android-layout"){
                  tag ="A layout defines the visual structure for a user interface, such as the UI for an activity, fragment or app widget.";
              }
              else if(Name =="spring-mvc"){
                  tag ="A framework for building Java web applications based on the Model-View-Controller (MVC) pattern. It promotes flexible and decoupled code from the underlying view technologies.";
              }
              else if(Name =="datetime"){
                  tag ="A DateTime object in many programming languages describes a date and a time of day. It can express either an instant in time or a position on a calendar, depending on the context in which it is used and the specific implementation. This tag can be used for all date and time related issues.";
              }
              else if(Name =="email"){
                  tag ="Email is a method of exchanging digital messages from a sender to one or more recipients. Posting to ask why the emails you send are marked as spam is off-topic for Stack Overflow";
              }
              else if(Name =="dictionary"){
                  tag ="A dictionary (or map) in computer science is a data structure that maps keys to values such that given a key its corresponding value can be efficiently retrieved. FOR questions about MAPPING FUNCTIONS over data, PLEASE USE [map-function] tag; and for geography, [maps]. ";
              }
              else if(Name =="tensorflow"){
                  tag ="**IMPORTANT**: PLEASE ADD THE LANGUAGE TAG YOU ARE DEVELOPING IN. TENSORFLOW SUPPORTS MORE THAN ONE LANGUAGE. TensorFlow is an open source library for machine learning and machine intelligence. It is developed by Google and became open source in November 2015.";
              }
              else if(Name =="jsp"){
                  tag ="JSP (JavaServer Pages) is a Java-based view technology running on the server machine which allows you to write template text in (the client side languages like HTML, CSS, JavaScript and so on) and interact with backend Java code.";
              }
              else if(Name =="oop"){
                  tag ="Object-oriented programming is a programming paradigm using 'objects': data structures consisting of data fields and methods together with their interactions.";
              }
              else if(Name =="wcf"){
                  tag ="Windows Communication Foundation is a part of the .NET Framework that provides a unified programming model for rapidly building service-oriented applications.";
              }
              else if(Name =="listview"){
                  tag ="A ListView is a graphical screen control or widget provided by UI libraries in a majority of modern operating systems to show items in a list form.";
              }
              else if(Name =="security"){
                  tag ="Topics relating to application security and attacks against software. Please don't use this tag alone, that results in ambiguity. If your question is not about a specific programming problem, please consider instead asking it at Information Security SE: https://security.stackexchange.com";
              }
              else if(Name =="parsing"){
                  tag ="Parsing refers to breaking an artifact into its constituent elements and capturing the relationship between those elements. This tag isn't for questions about the self hosted Parse Platform (use the [parse-platform] tag) or parse errors in a particular programming language (use the appropriate language tag instead). ";
              }
              else if(Name =="object"){
                  tag ="An object is any entity that can be manipulated by commands in a programming language. An object can be a value, a variable, a function, or a complex data-structure. In object-oriented programming, an object refers to an instance of a class. ";
              }
              else if(Name =="for-loop"){
                  tag ="A for loop is a control structure used by many programming languages to iterate over a range. It is a way of repeating statements a number of times until the loop ends. Depending on the language this may be over a range of integers, iterators, etc.";
              }
              else if(Name =="unity3d"){
                  tag ="";
              }
              else if(Name =="vue.js"){
                  tag ="Vue.js is an open-source, progressive Javascript framework for building user interfaces that aim to be incrementally adoptable. Vue.js is mainly used for front-end development and requires an intermediate level of HTML and CSS. Vue.js version specific questions should be tagged with [vuejs2] or [vuejs3].";
              }
              else if(Name =="user-interface"){
                  tag ="User Interface (UI) is the system through which people interact with a computer. This tag can be used for UI-related programming questions. Note that there's a separate Stack Exchange site for User Interfaces, Interactions with the Computer and User Experience design: https://ux.stackexchange.com.";
              }
              else if(Name =="ubuntu"){
                  tag ="GENERAL UBUNTU SUPPORT IS OFF-TOPIC. Support questions may be asked on https://askubuntu.com/. Ubuntu is a free desktop and server operating system based on Debian GNU/Linux. Note that this is for programming questions specific to Ubuntu and http://askubuntu.com is dedicated to answering general Ubuntu questions.";
              }
              else if(Name =="batch-file"){
                  tag ="A batch file is a text file containing a series of commands that are executed by the command interpreter on MS-DOS, IBM OS/2, or Microsoft Windows systems. ";
              }
              else if(Name =="visual-studio-2010"){
                  tag ="Visual Studio 2010 is an integrated development environment (IDE) from Microsoft. Use this tag only for questions arising from the use of this particular version of Visual Studio, and not about any code just written in it. ";
              }
              else if(Name =="pointers"){
                  tag ="Data type that 'points to anothe'r value stored in memory. A pointer variable contains a memory address of some other entity (variable or function or other entity). This tag should be used for questions involving the use of pointers, not references. The most common programming languages using pointers are C, C++, Go, and assembly languages. Use a specific language tag. Other helpful tags are method, function, struct, etc. describing the use of pointer.";
              }
              else if(Name =="templates"){
                  tag ="The templates tag is used in multiple contexts: generic programming (especially C++), and data/document generation using template engines. When using this tag on implementation heavy questions - tag the code language the implementation is written in. ";
              }
              else if(Name =="delphi"){
                  tag ="Delphi is a language for rapid development of native Windows, macOS, Linux, iOS, and Android applications through use of Object Pascal. The name refers to the Delphi language as well as its libraries, compiler and IDE which is used to help edit and debug Delphi projects.";
              }
              else if(Name =="if-statement"){
                  tag ="An 'if' statement is a control structure in many programming languages that changes the execution flow depending on a condition. Also Include an appropriate language tag such as 'java' if your question is language-specific. ";
              }
              else if(Name =="google-app-engine"){
                  tag ="Google App Engine is a cloud computing technology for hosting web applications in Google-managed data centers. Google App Engine is a Platform as a Service (PaaS) offering for Java, Python, Go, Node.js, and PHP in its standard environment. Runtimes for a few other languages as well as docker-based custom runtimes are supported in its flexible environment. ";
              }
              else if(Name =="ms-access×"){
                  tag ="Microsoft Access, also known as Microsoft Office Access, is an application development and database development tool from Microsoft. It combines the Microsoft Jet/ACE Database Engine with a graphical user interface and software-development tools. Other database engines, such as SQL Server, can also be used as a database server for Access applications.";
              }
              else if(Name =="variables"){
                  tag ="THIS IS AMBIGUOUS; USE SPECIFIC-LANGUAGE TAGS WHENEVER APPLICABLE. A variable is a named data storage location in memory. Using variables, a computer program can store numbers, text, binary data, or a combination of any of these data types. They can be passed around in the program.";
              }
              else if(Name =="matplotlib"){
                  tag ="Matplotlib is a plotting library for Python which may be used interactively or embedded in stand-alone GUIs. Its compact 'pyplot' interface is similar to the plotting functions of MATLAB®.";
              }
              else if(Name =="debugging"){
                  tag ="Debugging is a methodical process of finding and fixing bugs in a computer program. **IMPORTANT NOTE:** This tag is ONLY for questions about debugging techniques or the process of debugging itself, NOT for requesting help debugging your code.";
              }
              else if(Name =="haskell"){
                  tag ="Haskell is a functional programming language featuring strong static typing, lazy evaluation, extensive parallelism and concurrency support, and unique abstraction capabilities. ";
              }
              else if(Name =="go"){
                  tag ="Go is an open source programming language. It is statically-typed, with a syntax loosely derived from C, adding automatic memory management, type safety, some dynamic typing capabilities, additional built-in types such as variable-length arrays and key-value maps, and a large standard library.";
              }
              else if(Name =="authentication"){
                  tag ="Authentication is the process of determining whether someone or something is, in fact, who or what it is declared to be.";
              }
              else if(Name =="unix"){
                  tag ="This tag is EXCLUSIVELY for PROGRAMMING questions that are directly related to Unix; general software issues should be directed to the Unix & Linux Stack Exchange site or to Super User. The Unix operating system is a general-purpose OS that was developed by Bell Labs in the late 1960s and today exists in various versions.";
              }
              else if(Name =="elasticsearch"){
                  tag ="Elasticsearch is an Open Source (Apache 2), Distributed, RESTful, Search Engine based on Lucene.";
              }
              else if(Name =="asp.net-mvc-4"){
                  tag ="ASP.NET MVC 4 is the fourth major version of the ASP.NET Model-View-Controller platform for web applications.";
              }
              else if(Name =="hadoop"){
                  tag ="Hadoop is an Apache open-source project that provides software for reliable and scalable distributed computing. The core consists of a distributed file system (HDFS) and a resource manager (YARN). Various other open-source projects, such as Apache Hive use Apache Hadoop as persistence layer. ";
              }
              else if(Name =="session"){
                  tag ="A session refers to the communication between a single client and a server. A session is specific to the user and for each user a new session is created to track all the requests from that user.";
              }
              else if(Name =="actionscript-3"){
                  tag ="ActionScript 3 (AS3) is the open source object oriented programming (OOP) language of the Adobe Flash and AIR Platforms. AS3 is widely used for RIAs, mobile apps, and desktop applications. (ActionScript 3 is a dialect of ECMAScript.) ";
              }
              else if(Name =="pdf"){
                  tag ="Portable Document Format (PDF) is an open standard for electronic document exchange maintained by the International Organization for Standardization (ISO). Questions can be about creating, reading, editing PDFs using different languages. ";
              }
              else if(Name =="ssl"){
                  tag ="Secure Sockets Layer (SSL) is a cryptographic protocol that provides secure communications over the Internet. Often, SSL is used as a blanket term and refers to both the SSL protocol and the Transport Layer Security (TLS) protocol. The most recent version of the protocol is TLS version 1.3, specified by the IETF in RFC 8446.";
              }
              else if(Name =="jpa"){
                  tag ="The Java Persistence API (JPA) is a Java specification for accessing, persisting, and managing data between Java objects/classes and a relational database. It is part of the EJB 3.0 specification and is the industry standard approach for Object to Relational Mapping (ORM). ";
              }
              else if(Name =="magento"){
                 tag ="Magento is an e-commerce platform written in PHP atop the Zend framework. Questions should be related to writing code for Magento. General Magento questions may be asked on https://magento.stackexchange.com " ;
              }
              else if(Name =="url"){
                  tag ="A URL (Uniform Resource Locator), is a universal identifier on the web. A URL is a reference to a web resource at a specific location, and provides a means for retrieving that resource. ";
              }
              else if(Name =="testing"){
                  tag ="Software testing is any activity aimed at evaluating an attribute or capability of a program or system and determining that it meets its required results.";
              }
              else if(Name =="animation"){
                  tag ="Animation is the rapid display of a sequence of visuals in order to create an illusion of movement or change.";
              }
              else if(Name =="ionic-framework"){
                  tag ="Ionic is a front-end framework for developing native-feeling hybrid mobile apps with HTML and Sass. Traditionally, it runs on top of Cordova and Angular, but since Ionic 4 it supports Angular, React, Vue.js and Web Components running on top of Cordova or Capacitor.";
              }
              else if(Name =="laravel-5"){
                  tag ="Laravel 5 is the previous major version of the open-source PHP web development MVC framework created by Taylor Otwell. Laravel helps you create applications using simple, expressive syntax. Use the laravel tag for general Laravel related questions.";
              }
              else if(Name =="github"){
                  tag ="GitHub is a web-based hosting service for software development projects that use Git for version control. Use this tag for questions specific to problems with repositories hosted on GitHub, features specific to GitHub and using GitHub for collaborating with other users. Do not use this tag for Git-related issues simply because a repository happens to be hosted on GitHub. ";
              }
              else if(Name =="machine-learning"){
                  tag ="Implementation questions about machine learning algorithms. General questions about machine learning should be posted to their specific communities.";
              }
              else if(Name =="firefox"){
                  tag ="Mozilla Firefox is a free, open-source cross-platform web browser. Use this tag if your question is related to the inner workings of Firefox or if it relates to code that is not working on Firefox which does work in other browsers. Questions about Firefox add-on development should be tagged [firefox-addon]. If your question is about using Firefox for browsing (i.e. as an end user) you should ask your question on Super User instead.";
              }
              else if(Name =="inheritance"){
                  tag ="Inheritance is the system in object oriented programming that allows objects to support operations defined by anterior types without having to provide their own definition. It is the major vector for polymorphism in object-oriented programming. ";
              }
              else if(Name =="flash"){
                  tag ="For questions on Adobe's cross-platform multimedia runtime used to embed animations, video, and interactive applications into web pages. For questions related to memory, use the tag [flash-memory].";
              }
              else if(Name =="winapi"){
                  tag ="The Windows API (formerly called the Win32 API) is the core set of application programming interfaces available for the Microsoft Windows operating systems. This tag is for questions about developing native Windows applications using the Windows API. ";
              }
              else if(Name =="jsf"){
                  tag ="JavaServer Faces (JSF) is a model-view-presenter framework typically used to create HTML form based web applications. Using the standard components and render kit, stateful HTML views can be defined using Facelets or JSP tags and wired to model data and application logic via backing beans.";
              }
              else if(Name =="cocoa-touch"){
                  tag ="The Cocoa Touch Frameworks that drive iOS apps share many proven patterns found on the Mac, but were built with a special focus on touch-based interfaces and optimization.";
              }
              else if(Name =="post"){
                  tag ="POST is one of the HTTP protocol methods; it is used when the client needs to send data to the server, such as when uploading a file, or submitting a completed form. The word post has several meanings, but this tag is specifically about HTTP POST requests.";
              }
              else if(Name =="ipad"){
                  tag ="iPad is a tablet computer designed by Apple running the iOS operating system. iPad applications are usually written in Objective-C or Swift in the Xcode IDE, although it is also possible to use other tools to build iPad applications. Questions that are not dependent on hardware should use the iOS tag instead.";
              }
              else if(Name =="math"){
                  tag ="Math involves the manipulation of numbers within a program. For general math questions, please ask on https://math.stackexchange.com/";
              }
              else if(Name =="join"){
                  tag ="A JOIN is a general operation in relational algebra for a combining operation on two or more relations in a relational database system. JOIN is also keyword of the SQL language for performing this operation. ";
              }
              else if(Name =="dom"){
                  tag ="Use this tag on questions regarding the interaction of other languages with XML/HTML via the document object model. Do not use it as shorthand for HTML, JavaScript, or SAX—use additional tags to denote both language and markup. ";
              }
              else if(Name =="facebook-graph-api"){
                  tag ="";
              }
              else if(Name =="svg"){
                  tag ="Scalable Vector Graphics (SVG) is an XML-based two-dimensional vector graphics format that can also be used in HTML. Do not add this tag just because your project uses SVG. Instead, add the tag if your question is either about SVG, or closely related to it, like how to achieve something with SVG. ";
              }
              else if(Name =="opengl"){
                  tag ="OpenGL (Open Graphics Library) is a graphics standard and API which is platform independent and available for desktop, workstation and mobile devices. It is designed to provide hardware-accelerated rendering, and hence gives greatly improved performance over traditional software rendering. OpenGL is used for applications like CAD software and computer games. The OpenGL standard, as well as OpenGL ES, is controlled by the Khronos group.";
              }
              else if(Name =="xslt"){
                  tag ="XSLT is a transformation language for XML designed to transform structured documents into other formats (such as XML, HTML, and plain text, or, in XSLT 3, JSON). Questions should use one of the xslt-1.0, xslt-2.0, or xslt-3.0 tags as appropriate";
              }
              else if(Name =="image-processing"){
                  tag ="Anything related to digital image processing, i.e. the theory and the techniques used to extract or manipulate information from digital images";
              }
              else if(Name =="events"){
                  tag ="An event is a way for a class to provide notifications to listeners when a particular thing happens.";
              }
              else if(Name =="select"){
                  tag ="Select is a common keyword used to query data. 'select()' is also a programming function for triggering code based on file handle or other system activity. Do not use this tag for questions related to: HTML <select> tag (use [html-select]); language integrated query such as LINQ or similar, etc.";
              }
              else if(Name =="assembly"){
                  tag ="Assembly language (asm) programming questions. BE SURE TO ALSO TAG with the processor and/or instruction set you're using, as well as the assembler. WARNING: For .NET assemblies, use the tag [.net-assembly] instead. For Java ASM, use the tag [java-bytecode-asm] instead.";
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
            else if(Name == "validation"){
                date = new DateTime(2015,2,26);
            }
            else if(Name == "sockets"){
                date = new DateTime(2006,6,27);
            }
            else if(Name == "apache-spark"){
                date = new DateTime(2006,3,30);
            }
            else if(Name == "http"){
                date = new DateTime(2010,3,5);
            }
            else if(Name == "xaml"){
                date = new DateTime(2018,3,6);
            }
            else if(Name == "opencv"){
                date = new DateTime(2013,6,22);
            }
            else if(Name == "android-layout"){
                date = new DateTime(2003,9,8);
            }
            else if(Name == "spring-mvc"){
                date = new DateTime(2004,2,25);
            }
            else if(Name == "datetime"){
                date = new DateTime(2016,8,30);
            }
            else if(Name == "email"){
                date = new DateTime(2017,9,22);
            }
            else if(Name == "dictionary"){
                date = new DateTime(2018,6,3);
            }
            else if(Name == "tensorflow"){
                date = new DateTime(2000,9,28);
            }
            else if(Name == "jsp"){
                date = new DateTime(2009,11,20);
            }
            else if(Name == "oop"){
                date = new DateTime(2019,10,27);
            }
            else if(Name == "wcf"){
                date = new DateTime(2018,12,31);
            }
            else if(Name == "listview"){
                date = new DateTime(2018,11,22);
            }
            else if(Name == "security"){
                date = new DateTime(1999,10,1);
            }
            else if(Name == "parsing"){
                date = new DateTime(2009,6,27);
            }
            else if(Name == "object"){
                date = new DateTime(2010,5,6);
            }
            else if(Name == "for-loop"  ){
                date = new DateTime(2007,9,20);
            }
            else if(Name == "unity3d"){
                date = new DateTime(2017,6,27);
            }
            else if(Name == "vue.js"){
                date = new DateTime(2015,9,8);
            }
            else if(Name == "user-interface"){
                date = new DateTime(2002,12,20);
            }
            else if(Name == "ubuntu"){
                date = new DateTime(2010,10,28);
            }
            else if(Name == "batch-file"){
                date = new DateTime(2004,6,9);
            }
            else if(Name == "visual-studio-2010"){
                date = new DateTime(2013,9,10);
            }
            else if(Name == "pointers"){
                date = new DateTime(2001,10,23);
            }
            else if(Name == "templates"){
                date = new DateTime(2008,10,30);
            }
            else if(Name == "delphi"){
                date = new DateTime(2011,11,12);
            }
            else if(Name == "if-statement"){
                date = new DateTime(2000,2,24);
            }
            else if(Name == "google-app-engine"){
                date = new DateTime(2005,6,3);
            }
            else if(Name == "ms-access×"){
                date = new DateTime(2006,11,22);
            }
            else if(Name == "variables"){
                date = new DateTime(2010,2,8);
            }
            else if(Name == "matplotlib"){
                date = new DateTime(2019,6,22);
            }
            else if(Name == "debugging"){
                date = new DateTime(2005,3,14);
            }
            else if(Name == "haskell"){
                date = new DateTime(2016,10,28);
            }
            else if(Name == "go"){
                date = new DateTime(2000,3,9);
            }
            else if(Name == "authentication"){
                date = new DateTime(2006,4,6);
            }
            else if(Name == "unix"){
                date = new DateTime(2005,3,9);
            }
            else if(Name == "elasticsearch"){
                date = new DateTime(2001,8,9);
            }
            else if(Name == "asp.net-mvc-4"){
                date = new DateTime(2009,6,28);
            }
            else if(Name == "hadoop"){
                date = new DateTime(2004,2,8);
            }
             else if(Name =="session" ){
                date = new DateTime(1996,6,30);
            }
             else if(Name == "actionscript-3"){
                date = new DateTime(2008,7,7);
            }
             else if(Name == "pdf"){
                date = new DateTime(2013,11,12);
            }
             else if(Name == "ssl"){
                date = new DateTime(2013,1,27);
            }
             else if(Name == "jpa"){
                date = new DateTime(2000,1,1);
            }
             else if(Name == "magento"){
                date = new DateTime(2003,3,30);
            }
             else if(Name == "url"){
                date = new DateTime(2012,3,25);
            }
             else if(Name == "testing"){
                date = new DateTime(2003,3,28);
            }
             else if(Name == "animation"){
                date = new DateTime(2002,2,22);
            }
             else if(Name == "ionic-framework"){
                date = new DateTime(2011,8,18);
            }
             else if(Name == "laravel-5"){
                date = new DateTime(2009,9,9);
            }
             else if(Name == "github"){
                date = new DateTime(2013,11,11);
            }
             else if(Name == "machine-learning"){
                date = new DateTime(2014,4,14);
            }
            else if(Name == "firefox"){
                date = new DateTime(2003,3,23);
            }
            else if(Name == "inheritance"){
                date = new DateTime(2014,2,27);
            }
            else if(Name == "flash"){
                date = new DateTime(2016,6,6);
            }
            else if(Name == "winapi"){
                date = new DateTime(2015,2,28);
            }
            else if(Name == "jsf"){
                date = new DateTime(2014,11,22);
            }
             else if(Name =="cocoa-touch" ){
                date = new DateTime(2013,6,29);
            }
             else if(Name == "post"){
                date = new DateTime(2008,6,6);
            }
             else if(Name == "ipad"){
                date = new DateTime(2005,5,9);
            }
             else if(Name == "math"){
                date = new DateTime(2007,7,7);
            }
             else if(Name == "join"){
                date = new DateTime(2010,8,28);
            }
             else if(Name == "dom"){
                date = new DateTime(2011,11,19);
            }
             else if(Name == "facebook-graph-api" ){
                date = new DateTime(2010,10,25);
            }
             else if(Name == "svg" ){
                date = new DateTime(2003,3,3);
            }
            else if(Name == "opengl" ){
                date = new DateTime(1999,9,9);
            }
            else if(Name == "xslt"){
                date = new DateTime(1998,9,7);
            }
            else if(Name == "image-processing"){
                date = new DateTime(2000,5,9);
            }
            else if(Name == "events"){
                date = new DateTime(2005,8,9);
            }
             else if(Name == "select"){
                date = new DateTime(2001,2,22);
            } else if(Name == "assembly"){
                date = new DateTime(2006,8,29);
            }

            else {
                date = DateTime.Now;
            }
            return date;
        }
        }
       
    }
}