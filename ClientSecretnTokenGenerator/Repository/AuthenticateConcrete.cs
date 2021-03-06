﻿using System;
using System.Linq;
using ClientSecretnTokenGenerator.Models;
using ClientSecretnTokenGenerator.Context;
using ClientSecretnTokenGenerator.AES256Encryption;
using static ClientSecretnTokenGenerator.AES256Encryption.EncryptionLibrary;

namespace ClientSecretnTokenGenerator.Repository
{
    public class AuthenticateConcrete : IAuthenticate
    {
        DatabaseContext _context;

        public AuthenticateConcrete()
        {
            _context = new DatabaseContext();
        }

        public ClientKey GetClientKeysDetailsbyCLientIDandClientSecert(string clientID, string clientSecert)
        {
            try
            {
                var result = (from clientkeys in _context.ClientKeys
                              where clientkeys.ClientID == clientID && clientkeys.ClientSecret == clientSecert
                              select clientkeys).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ValidateKeys(ClientKey ClientKeys)
        {
            try
            {
                var result = (from clientkeys in _context.ClientKeys
                              where clientkeys.ClientID == ClientKeys.ClientID && clientkeys.ClientSecret == ClientKeys.ClientSecret
                              select clientkeys).Count();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsTokenAlreadyExists(int CompanyID)
        {
            try
            {
                var result = (from token in _context.TokensManager
                              where token.CompanyID == CompanyID
                              select token).Count();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int DeleteGenerateToken(int CompanyID)
        {
            try
            {
                var token = _context.TokensManager.SingleOrDefault(x => x.CompanyID == CompanyID);
                _context.TokensManager.Remove(token);
                return _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GenerateToken(ClientKey ClientKeys, DateTime IssuedOn)
        {
            try
            {
                string randomnumber =
                   string.Join(":", new string[]
                   {   Convert.ToString(ClientKeys.UserID),
                KeyGenerator.GetUniqueKey(),
                Convert.ToString(ClientKeys.CompanyID),
                Convert.ToString(IssuedOn.Ticks),
                ClientKeys.ClientID
                   });

                return EncryptionLibrary.EncryptText(randomnumber);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int InsertToken(TokensManager token)
        {
            try
            {
                _context.TokensManager.Add(token);
                return _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}