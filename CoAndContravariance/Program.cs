using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoAndContravariance
{
    /// <summary>
    /// https://msdn.microsoft.com/en-us/library/dd799517(v=vs.110).aspx
    /// Covariance:
    ///    Enables you to use a more derived type than originally specified.
    ///    You can assign an instance of IEnumerable<Derived> (IEnumerable(Of Derived) in Visual Basic) to a variable of type IEnumerable<Base>.
    ///    Covariant type parameters enable you to make assignments that look much like ordinary Polymorphism
    ///    IEnumerable<Derived> d = new List<Derived>();
    ///    IEnumerable<Base> b = d;
    /// Contravariance:
    ///    Enables you to use a more generic (less derived) type than originally specified.
    ///    You can assign an instance of IEnumerable<Base> (IEnumerable(Of Base) in Visual Basic) to a variable of type IEnumerable<Derived>.
    ///    Action<Base> b = (target) => { Console.WriteLine(target.GetType().Name); };
    ///    Action<Derived> d = b;
    ///    d(new Derived());
    /// Invariance:
    ///    Means that you can use only the type originally specified; so an invariant generic type parameter is neither covariant nor contravariant.
    ///    You cannot assign an instance of IEnumerable<Base> (IEnumerable(Of Base) in Visual Basic) to a variable of type IEnumerable<Derived> or vice versa.
    /// </summary>
    class CoAndContravarianceInDelegates
    {
        class Person { };
        class Employee : Person { };


        #region covariance

        delegate Person ReturnPersonDelegate();
        ReturnPersonDelegate returnPersonMethod;

        Employee ReturnEmployee()
        {
            return new Employee();
        }

        /// <summary>
        /// Covariance lets a method return a value from a subclass of the result expected by a delegate.
        /// </summary>
        void CovarianceExample()
        {
            returnPersonMethod = ReturnEmployee;
        }
        #endregion covariance



        #region contravariance

        delegate void EmployeeParameterDelegate(Employee employee);
        EmployeeParameterDelegate employeeParameterMethod;

        void PersonParameter(Person person) { }

        /// <summary>
        /// Contravariance lets a method take parameters that are from a superclass of the type expected by a delegate.
        /// </summary>
        void ContravarianceExample()
        {
            employeeParameterMethod = PersonParameter;
        }

        #endregion contravariance
    }

    class CoAndContravariantInGenerics {

        #region covariance in generics
        // For generic type parameters, the out keyword specifies that the type parameter is covariant. 
        // You can use the out keyword in generic interfaces and delegates.

        // A covariant type parameter is marked with the out keyword.
        // You can use a covariant type parameter as the return value of a method that belongs to an interface, 
        // or as the return type of a delegate. You cannot use a covariant type parameter as a generic type constraint for interface methods.

        // Covariant interface.
        interface ICovariant<out R> { }

        // Extending covariant interface.
        interface IExtCovariant<out R> : ICovariant<R> { }

        // Implementing covariant interface.
        class Sample<R> : ICovariant<R> { }

        class Program
        {
            static void Test()
            {
                ICovariant<Object> iobj = new Sample<Object>();
                ICovariant<String> istr = new Sample<String>();

                // You can assign istr to iobj because
                // the ICovariant interface is covariant.
                iobj = istr;
            }
        }
        #endregion covariance in generics

        #region contravariance in generics

        // A contravariant type parameter is marked with the in keyword. 
        // You can use a contravariant type parameter as the type of a parameter of a method that belongs to an interface, 
        // or as the type of a parameter of a delegate. You can use a contravariant type parameter as a generic type constraint for an interface method.

        // Contravariant interface.
        interface IContravariant<in R> { }

        // Extending contravariant interface.
        interface IExtContravariant<in R> : IContravariant<R> { }

        // Implementing contravariant interface.
        class Sample2<R> : IContravariant<R> { }

        class Program2
        {
            static void Test()
            {
                IContravariant<Object> iobj = new Sample2<Object>();
                IContravariant<String> istr = new Sample2<String>();

                // You can assign iobj to istr because
                // the IContravariant interface is contravariant.
                istr = iobj;
            }
        }


        // Delegates

        /// The type of the objects to compare.This type parameter is contravariant. That
        /// is, you can use either the type you specified or any type that is less derived.
        /// For more information about covariance and contravariance, see Covariance and
        /// Contravariance in Generics.
        public delegate int Comparison<in T>(T x, T y);

        #endregion contravariance in generics
    }
}
