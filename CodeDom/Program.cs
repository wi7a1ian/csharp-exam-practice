using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Xml;
using System.IO;


namespace CodeDom
{
    /// <summary>
    /// http://msdn.microsoft.com/en-us/library/system.codedom.codetypedeclaration(v=vs.110).aspx
    /// http://stackoverflow.com/questions/15547525/creating-a-class-with-codetypedeclaration-and-adding-members-to-it
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Creates a new type declaration.
            CodeTypeDeclaration newType = new CodeTypeDeclaration("TestType"); // // name parameter indicates the name of the type.
            newType.Attributes = MemberAttributes.Public | MemberAttributes.Final; // Sets the member attributes for the type to private.
            newType.BaseTypes.Add("String"); // // Sets a base class which the type inherits from.
            newType.IsClass = true;

            // Add member
            CodeMemberField myField = new CodeMemberField();
            myField.Name = "SomeField";
            myField.Type = new CodeTypeReference(typeof(string));//new CodeTypeReference("string");
            newType.Members.Add(myField);

            // Method with a code snippet
            CodeMemberMethod method = new CodeMemberMethod();
            method.Name = "DoSomething";
            method.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            method.Statements.Add(new CodeSnippetStatement("var n = 2;"));
            newType.Members.Add(method);

            // Constants
            CodeTypeDeclaration exampleClass = new CodeTypeDeclaration("GeneratedClass");
            CodeMemberField constant = new CodeMemberField(new CodeTypeReference(typeof(System.UInt32)), "addressFilteresErrorCounters");
            constant.Attributes = MemberAttributes.Const;
            constant.InitExpression = new CodePrimitiveExpression(0x0000AE77);
            exampleClass.Members.Add(constant);


            // Compilers
            CodeDomProvider provider = CodeDomProvider.CreateProvider("C#");
            CodeCompileUnit compileUnit = new CodeCompileUnit();
            CodeGeneratorOptions options = new CodeGeneratorOptions
            {
                BlankLinesBetweenMembers = false,
                BracingStyle = "C",
                VerbatimOrder = false,
            };

            // Namespace
            CodeNamespace namespace2 = new CodeNamespace();
            //CodeNamespace namespace3 = new CodeNamespace(this.File.Context.Namespace);
            namespace2.Imports.Add(new CodeNamespaceImport("System"));
            namespace2.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            compileUnit.Namespaces.Add(namespace2);
            namespace2.Types.Add(newType);

            // Save to file
            using (StreamWriter writer = File.CreateText(XmlConvert.EncodeName("TestType") + ".cs"))
                provider.GenerateCodeFromCompileUnit(compileUnit, writer, options);

            Console.Write(File.ReadAllText("TestType.cs"));
            Console.ReadKey();
        }
    }
}
