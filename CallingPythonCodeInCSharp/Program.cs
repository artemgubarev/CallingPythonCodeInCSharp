using System;

namespace CallingPythonCodeInCSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PythonIterop pythonIterop = new PythonIterop();

            // example 1, run python code
            string pycodePath_1 = @"..\..\..\PythonFiles\example1.py";
            pythonIterop.RunPythonCode(pycodePath_1);

            // example 2, run python code with parameters
            // output sum of two numbers
            string pycodePath_2 = @"..\..\..\PythonFiles\example2.py";
            // variables names
            string[] parametersNames_1 = new string[]
            {
                "number_1",
                "number_2"
            };
            // variables values
            object[] parameters_1 = new object[]
            {
                2,
                4 
            };
            pythonIterop.RunPythonCodeWithParams(pycodePath_2, parametersNames_1, parameters_1);


            // example 3, run python code with parameters and returning value
            // output sum of two numbers
            string pycodePath_3 = @"..\..\..\PythonFiles\example3.py";
            // variables names
            string[] parametersNames_2 = new string[]
            {
                "number",
            };
            // variables values
            object[] parameters_2 = new object[]
            {
                9
            };

            // computate square of the number
            string returningVariableName = "_square";
            var result = pythonIterop.
                         RunPythonCodeWithParamsAndReturningValue(pycodePath_3, parametersNames_2, 
                                                                  parameters_2, returningVariableName);
            Console.WriteLine("Python code with parametes and returning value " +
                "completed successfully, result = {0}", result);
        }
    }
}
