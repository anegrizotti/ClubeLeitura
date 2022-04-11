using System;
using System.Collections.Generic;

namespace ClubeLeitura.ConsoleApp.Compartilhado
{
    public interface ISerializavel<T> where T : EntidadeBase
    {

        void SerializarObjeto(T entidade);

        T DeserializarObjeto(string arquivoJSON);

        void SerializarListaDeObjetos(List<T> entidade);

        List<T> DeserializarListaDeObjetos(string arquivoJSON);

    }

}
