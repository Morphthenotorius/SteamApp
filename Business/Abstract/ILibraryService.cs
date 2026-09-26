using Business.DTOs.LibraryDTO;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ILibraryService : IServiceBase<GetLibraryDTO,CreateLibraryDTO,UpdateLibraryDTO,Library>
    {
    }
}
