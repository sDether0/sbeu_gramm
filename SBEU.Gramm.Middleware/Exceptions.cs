using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBEU.Gramm.DataLayer.DataBase.Entities;
using SBEU.Gramm.DataLayer.DataBase.Entities.Base;
using SBEU.Gramm.DataLayer.DataBase.Entities.Interfaces;

namespace SBEU.Gramm.Middleware
{
    public static class Exceptions
    {
        public static Exception EntityNotFound<T>() where T : IBaseEntity =>
            new EntityNotFoundException<T>($"{typeof(T)} not found");

        public static Exception NullEditArgument() =>
            new NullArgumentException("Empty edit info");

        public static Exception NullCreateArgument() =>
            new NullArgumentException("Empty create info");

        public static Exception InvalidArgument<T>() where T : IBaseEntity =>
            new InvalidArgumentException<T>($"Invalid or empty {typeof(T)} {nameof(IBaseEntity.Id)}");

        public static Exception EntityDeleted<T>() where T : IBaseEntity =>
            new EntityDeletedException<T>($"{typeof(T)} is deleted");

        public static Exception AlreadyExist<T>() where T : IBaseEntity =>
            new AlreadyExistException<T>($"Exact {typeof(T)} already exist");

        /// <summary>
        /// It returns the HTTP status code that corresponds to the exception that was thrown
        /// </summary>
        /// <param name="Exception">The exception that is being thrown.</param>
        /// <returns>
        /// The status code of the exception.
        /// </returns>
        public static int Status(this Exception e) 
        {
            switch (e)
            {
                case EntityNotFoundException<NPost>:
                case EntityNotFoundException<NStory>:
                case EntityNotFoundException<NCommentary>:
                case EntityNotFoundException<NLike>:
                case EntityNotFoundException<NContentObject>:
                    return 404;
                case AlreadyExistException<NPost>:
                case AlreadyExistException<NStory>:
                case AlreadyExistException<NCommentary>:
                case AlreadyExistException<NLike>:
                case AlreadyExistException<NContentObject>:
                    return 409;
                case InvalidArgumentException<NPost>:
                case InvalidArgumentException<NStory>:
                case InvalidArgumentException<NCommentary>:
                case InvalidArgumentException<NLike>:
                case InvalidArgumentException<NContentObject>:
                case NullArgumentException:
                    return 422;
                default: return 400;
            }
        }
    }

    public class BaseSbeuException : Exception
    {
        public BaseSbeuException(string error) : base(error) { }
    }

    public class EntityNotFoundException<T> : BaseSbeuException where T : IBaseEntity
    {
        public EntityNotFoundException(string error) : base(error) { }
    }


    public class NullArgumentException : BaseSbeuException
    {
        public NullArgumentException(string error) : base(error) { }
    }

    public class InvalidArgumentException<T> : BaseSbeuException where T : IBaseEntity
    {
        public InvalidArgumentException(string error) : base(error) { }
    }

    public class EntityDeletedException<T> : BaseSbeuException where T : IBaseEntity
    {
        public EntityDeletedException(string error) : base(error) { }
    }

    public class AlreadyExistException<T> : BaseSbeuException where T : IBaseEntity
    {
        public AlreadyExistException(string error) : base(error) { }
    }


}
