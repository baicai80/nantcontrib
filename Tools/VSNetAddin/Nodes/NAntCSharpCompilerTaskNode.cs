//
// NAntContrib - NAntAddin
// Copyright (C) 2002 Jayme C. Edwards (jedwards@wi.rr.com)
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
// Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with this library; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307 USA
//

using System;
using System.Xml;
using System.ComponentModel;
using System.Windows.Forms;

namespace NAnt.Contrib.NAntAddin.Nodes
{
	/// <summary>
	/// Tree Node that represents an NAnt csc task.
	/// </summary>
	/// <remarks>None.</remarks>
	[NAntTask("csc", "Compile a C# Program with Microsoft's Compiler", "csharptask.bmp")]
	public class NAntCSharpCompilerTaskNode : NAntTaskNode
	{
		/// <summary>
		/// Creates a new <see cref="NAntCSharpCompilerTaskNode"/>
		/// </summary>
		/// <param name="TaskElement">The task's XML element.</param>
		/// <param name="ParentElement">The parent XML element of the task.</param>
		/// <remarks>None.</remarks>
		public NAntCSharpCompilerTaskNode(XmlElement TaskElement, XmlElement ParentElement) 
			: base(TaskElement, ParentElement)
		{
		}

		/// <summary>
		/// Gets or sets the type of file generated at compilation.
		/// </summary>
		/// <value>The type of file generated at compilation.</value>
		/// <remarks>None.</remarks>
		[Description("The type of file generated at compilation."),Category("Behavior")]
		public CSharpCompilerTarget Target
		{
			get
			{
				return TaskElement.GetAttribute("target") == "library" ? CSharpCompilerTarget.Library : CSharpCompilerTarget.Executable;
			}

			set
			{
				TaskElement.SetAttribute("target", value == CSharpCompilerTarget.Library ? "library" : "exe");
				Save();
			}
		}

		/// <summary>
		/// Gets or sets the name of the file generated after compilation.
		/// </summary>
		/// <value>The name of the file generated after compilation.</value>
		/// <remarks>None.</remarks>
		[Description("The name of the file generated after compilation."),Category("Data")]
		public string Output
		{
			get
			{
				return TaskElement.GetAttribute("output");
			}

			set
			{
				if (value == "")
				{
					TaskElement.RemoveAttribute("output");
				}
				else
				{
					TaskElement.SetAttribute("output", value);
				}
				Save();
			}
		}

		/// <summary>
		/// Gets or sets the XML documentation file to generate.
		/// </summary>
		/// <value>The XML documentation file to generate.</value>
		/// <remarks>None.</remarks>
		[Description("XML documentation file to generate."),Category("Data")]
		public string Doc
		{
			get
			{
				return TaskElement.GetAttribute("doc");
			}

			set
			{
				if (value == "")
				{
					TaskElement.RemoveAttribute("doc");
				}
				else
				{
					TaskElement.SetAttribute("doc", value);
				}
				Save();
			}
		}

		/// <summary>
		/// Gets or sets conditional compilation symbol(s) to set.
		/// </summary>
		/// <value>Condition compilation symbol(s) to set.</value>
		/// <remarks>None.</remarks>
		[Description("Conditional compilation symbol(s) to set."),Category("Data")]
		public string Define
		{
			get
			{
				return TaskElement.GetAttribute("define");
			}

			set
			{
				if (value == "")
				{
					TaskElement.RemoveAttribute("define");
				}
				else
				{
					TaskElement.SetAttribute("define", value);
				}
				Save();
			}
		}

		/// <summary>
		/// Gets or sets the icon to use for a Windows application.
		/// </summary>
		/// <value>The icon to use for a Windows application.</value>
		/// <remarks>None.</remarks>
		[Description("Icon to use for a Windows Application."),Category("Appearance")]
		public string Win32Icon
		{
			get
			{
				return TaskElement.GetAttribute("win32icon");
			}

			set
			{
				if (value == "")
				{
					TaskElement.RemoveAttribute("win32icon");
				}
				else
				{
					TaskElement.SetAttribute("win32icon", value);
				}
				Save();
			}
		}

		/// <summary>
		/// Gets or sets if debug output should be generated.
		/// </summary>
		/// <value>If debug output should be generated.</value>
		/// <remarks>None.</remarks>
		[Description("If debug output should be generated."),Category("Behavior")]
		public string Debug
		{
			get
			{
				return TaskElement.GetAttribute("debug");
			}

			set
			{
				if (value == "")
				{
					TaskElement.RemoveAttribute("debug");
				}
				else
				{
					TaskElement.SetAttribute("debug", value);
				}
				Save();
			}
		}

		/// <summary>
		/// Gets or sets assemblies to reference during compilation.
		/// </summary>
		/// <value>Assemblies to reference during compilation.</value>
		/// <remarks>None.</remarks>
		[Description("Assemblies to reference during compilation."),Category("Data")]
		public References References
		{
			get
			{
				References references = new References(TaskElement, this);
				if (Parent == null)
				{
					return (References)NAntReadOnlyNodeBuilder.GetReadOnlyNode(references);
				}
				return references;
			}

			set
			{
				value.AppendToTask(TaskElement, "references");
				Save();
			}
		}

		/// <summary>
		/// Gets or sets resource files to embed.
		/// </summary>
		/// <value>Resource files to embed.</value>
		/// <remarks>None.</remarks>
		[Description("Resource files to embed."),Category("Data")]
		public Resources Resources
		{
			get
			{
				Resources resources = new Resources(TaskElement, this);
				if (Parent == null)
				{
					return (Resources)NAntReadOnlyNodeBuilder.GetReadOnlyNode(resources);
				}
				return resources;
			}

			set
			{
				value.AppendToTask(TaskElement, "resources");
				Save();
			}
		}

		/// <summary>
		/// Gets or sets modules to link to the assembly.
		/// </summary>
		/// <value>Modules to link to the assembly.</value>
		/// <remarks>None.</remarks>
		[Description("Modules to link to the assembly."),Category("Data")]
		public Modules Modules
		{
			get
			{
				Modules modules = new Modules(TaskElement, this);
				if (Parent == null)
				{
					return (Modules)NAntReadOnlyNodeBuilder.GetReadOnlyNode(modules);
				}
				return modules;
			}

			set
			{
				value.AppendToTask(TaskElement, "modules");
				Save();
			}
		}

		/// <summary>
		/// Gets or sets source files to compile.
		/// </summary>
		/// <value>Source files to compile.</value>
		/// <remarks>None.</remarks>
		[Description("Source files to compile."),Category("Data")]
		public Sources Sources
		{
			get
			{
				Sources sources = new Sources(TaskElement, this);
				if (Parent == null)
				{
					return (Sources)NAntReadOnlyNodeBuilder.GetReadOnlyNode(sources);
				}
				return sources;
			}

			set
			{
				value.AppendToTask(TaskElement, "sources");
				Save();	
			}
		}
	}

	/// <summary>
	/// An <see cref="NAntFileSet"/> that specifies referenced Microsoft.NET assemblies.
	/// </summary>
	/// <remarks>None.</remarks>
	public class References : NAntFileSet
	{
		/// <summary>
		/// Creates a new <see cref="References"/>.
		/// </summary>
		/// <param name="TaskElement">The references XML element.</param>
		/// <param name="TaskNode">The <see cref="NAntTaskNode"/> for which this <see cref="References"/> is a property.</param>
		/// <remarks>None.</remarks>
		public References(XmlElement TaskElement, NAntTaskNode TaskNode) : base(TaskNode, TaskElement, "references")
		{
			
		}
	}

	/// <summary>
	/// An <see cref="NAntFileSet"/> that specifies resources.
	/// </summary>
	/// <remarks>None.</remarks>
	public class Resources : NAntFileSet
	{
		/// <summary>
		/// Creates a new <see cref="Resources"/>.
		/// </summary>
		/// <param name="TaskElement">The resources XML element.</param>
		/// <param name="TaskNode">The <see cref="NAntTaskNode"/> for which this <see cref="Resources"/> is a property.</param>
		/// <remarks>None.</remarks>
		public Resources(XmlElement TaskElement, NAntTaskNode TaskNode) : base(TaskNode, TaskElement, "resources")
		{
			
		}
	}

	/// <summary>
	/// An <see cref="NAntFileSet"/> that specifies Microsoft.NET modules.
	/// </summary>
	/// <remarks>None.</remarks>
	public class Modules : NAntFileSet
	{
		/// <summary>
		/// Creates a new <see cref="Modules"/>.
		/// </summary>
		/// <param name="TaskElement">The modules XML element.</param>
		/// <param name="TaskNode">The <see cref="NAntTaskNode"/> for which this <see cref="Modules"/> is a property.</param>
		/// <remarks>None.</remarks>
		public Modules(XmlElement TaskElement, NAntTaskNode TaskNode) : base(TaskNode, TaskElement, "modules")
		{
			
		}
	}

	/// <summary>
	/// An <see cref="NAntFileSet"/> that specifies source files.
	/// </summary>
	/// <remarks>None.</remarks>
	public class Sources : NAntFileSet
	{
		/// <summary>
		/// Creates a new <see cref="Sources"/>.
		/// </summary>
		/// <param name="TaskElement">The sources XML element.</param>
		/// <param name="TaskNode">The <see cref="NAntTaskNode"/> for which this <see cref="Sources"/> is a property.</param>
		/// <remarks>None.</remarks>
		public Sources(XmlElement TaskElement, NAntTaskNode TaskNode) : base(TaskNode, TaskElement, "sources")
		{
			
		}
	}

	/// <summary>
	/// Defines constants for specifying the target of a compile.
	/// </summary>
	/// <remarks>None.</remarks>
	public enum CSharpCompilerTarget
	{
		/// <summary>
		/// The target is a library (.dll).
		/// </summary>
		Library,
		/// <summary>
		/// The target is an executable (.exe).
		/// </summary>
		Executable
	}
}