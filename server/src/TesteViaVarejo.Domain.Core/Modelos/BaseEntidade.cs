using FluentValidation;
using FluentValidation.Results;
using System;

namespace Sage.Comtax.API.Domain.Core.Modelos
{
    public abstract class BaseEntidade<T> : AbstractValidator<T> where T : BaseEntidade<T>
    {
        #region Ctor
        public BaseEntidade()
        {
            DataCriacao = DateTime.Now;
            ValidationResult = new ValidationResult();
        }
        #endregion

        #region Properties
        public Int32 Id { get; protected set; }
        public DateTime DataCriacao { get; set; }
        public ValidationResult ValidationResult { get; set; }

        public abstract bool EhValido();

        #endregion

        #region Nav Prop


        public override bool Equals(object obj)
        {
            var compareTo = obj as BaseEntidade<T>;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(BaseEntidade<T> a, BaseEntidade<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(BaseEntidade<T> a, BaseEntidade<T> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 967) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + "[Id = " + Id + "]";
        }
        #endregion
    }
}