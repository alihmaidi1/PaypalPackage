using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.Abstraction;
public class HttpClientDisposeAbstraction:IDisposable
{


    protected  HttpClient httpClient{get;set;}

    private bool isDisposed{get;set;}=false;


    ~HttpClientDisposeAbstraction(){

        Dispose(false);
    }

    public void Dispose()
    {

        Dispose(true);
        GC.SuppressFinalize(this);

    }




    protected virtual void Dispose(bool disposing)
    {
        if (isDisposed)
            return;

        if(disposing){

            this.httpClient?.Dispose();

        }
        isDisposed=true;
    }

    
}
