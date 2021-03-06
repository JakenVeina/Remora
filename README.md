 Remora
 ======

Remora is a collection of common libraries and modules, useful for a variety of common applications and tools. Each
component of Remora is intended to be a standalone product, but may have dependencies on other Remora modules. All
modules are independently released on NuGet.

All libraries are compatible with .NET Standard 2.0, and are built with C# 8 and nullable reference types in mind.

## Table of Contents
  * [Remora.Results.Abstractions](Remora.Results.Abstractions) - Abstractions and public interfaces for a descriptive
  algebraic data type for C#.
  * [Remora.Results](Remora.Results) - The default implementation of Remora.Results.Abstractions, containing various
  generic and useful result types.
  * [Remora.Behaviours](Remora.Behaviours) - A small library for service-friendly behavioural programming.
  * [Remora.Plugins](Remora.Plugins) - The default implementation of Remora.Plugins.Abstractions.
  * [Remora.Plugins.Abstractions](Remora.Plugins.Abstractions) - Abstractions and public interfaces for a dynamic plugin
  system, allowing independently developed application modules to coexist.
  * [Remora.EntityFrameworkCore.Modular](Remora.EntityFrameworkCore.Modular) - An implementation of a modular approach
  to EF Core, allowing independent modules of an application to coexist in a single database with no crosstalk and
  intuitive dependencies.
