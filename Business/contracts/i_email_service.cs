using Business.Implementations;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface IEmailService
    {

    bool sendMail(
        int id,
        EmailClass emailClass);

    }

}