using System;
using System.Collections.Generic;
using LibraryData.Models;
using LibraryData;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LibraryServices
{
    public class LibraryAssetService : ILibraryAsset
    {
        private LibraryContext _context;

        public LibraryAssetService(LibraryContext context)
        {
            _context = context;
        }

        public void AddLibraryAsset(LibraryAsset newAsset)
        {
            _context.Add(newAsset);
            _context.SaveChanges();
        }

        public IEnumerable<LibraryAsset> GetAll()
        {
            return _context.LibraryAssets
                .Include(asset => asset.Status)
                .Include(asset => asset.Location); //returns statuses and location of assets
        }

       

        public LibraryAsset GetById(int id)
        {
            return _context.LibraryAssets
                .Include(asset => asset.Status)
                .Include(asset => asset.Location)
                .FirstOrDefault(asset => asset.Id == id); 
        }

        public string GetDeweyIndex(int id) //discrimator column because of abstract class
        {
            if (_context.Books.Any(Book => Book.Id == id))
            {
                return _context.Books.FirstOrDefault(Book => Book.Id == id).DeweyIndex;
            }
            else return "";
        }

        public string GetIsbn(int id)
        {
            if (_context.Books.Any(Book => Book.Id == id))
            {
                return _context.Books.FirstOrDefault(Book => Book.Id == id).ISBN;
            }
            else return "";
        }

        public LibraryBranch GetLibraryBranch(int id)
        {
            return GetById(id).Location;

        }

        public string GetTitle(int id)
        {
            return GetById(id).Title;
        }

        public string GetType(int id)
        {
            if (_context.Books.Any(Book => Book.Id == id))
            {
                return "Book";
            }
            else return "Video";
        }
        public string GetAuthorOrDirectorById(int id)
        {
            if (_context.Books.Any(Book => Book.Id == id))
            {
                return _context.Books.FirstOrDefault(Book => Book.Id == id).Author;
            }
            else return _context.Videos.FirstOrDefault(Video => Video.Id == id).Director;
        }
    }

}
