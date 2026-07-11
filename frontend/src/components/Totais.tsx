import { useEffect, useState } from "react";
import api from "../api/api";

/*
 Representa os totais de cada pessoa.
*/
interface TotalPessoa {
    pessoaId: number;
    nome: string;
    totalReceitas: number;
    totalDespesas: number;
    saldo: number;
}

/*
 Representa a resposta da API.
*/
interface RespostaTotais {
    pessoas: TotalPessoa[];
    totalReceitas: number;
    totalDespesas: number;
    saldoLiquido: number;
}

function Totais() {

    const [dados, setDados] = useState<RespostaTotais | null>(null);

    async function carregarTotais() {

        try {

            const response = await api.get("/Totais");

            setDados(response.data);

        }
        catch {

            alert("Erro ao carregar os totais.");

        }

    }

    useEffect(() => {

        carregarTotais();

    }, []);

    return (

        <div className="card">

            <div className="titulo">

                <h2>Consulta de Totais</h2>

                <button onClick={carregarTotais}>

                    Atualizar

                </button>

            </div>

            <table>

                <thead>

                    <tr>

                        <th>Pessoa</th>
                        <th>Receitas</th>
                        <th>Despesas</th>
                        <th>Saldo</th>

                    </tr>

                </thead>

                <tbody>

                    {dados?.pessoas.map((pessoa) => (

                        <tr key={pessoa.pessoaId}>

                            <td>{pessoa.nome}</td>

                            <td>
                                {pessoa.totalReceitas.toLocaleString("pt-BR",
                                    {
                                        style: "currency",
                                        currency: "BRL"
                                    })}
                            </td>

                            <td>
                                {pessoa.totalDespesas.toLocaleString("pt-BR",
                                    {
                                        style: "currency",
                                        currency: "BRL"
                                    })}
                            </td>

                            <td>

                                {pessoa.saldo.toLocaleString("pt-BR",
                                    {
                                        style: "currency",
                                        currency: "BRL"
                                    })}

                            </td>

                        </tr>

                    ))}

                </tbody>

            </table>

            <div className="resumo">

                <h3>Total Geral</h3>

                <p>

                    <strong>Receitas:</strong>

                    {" "}

                    {dados?.totalReceitas.toLocaleString("pt-BR",
                        {
                            style: "currency",
                            currency: "BRL"
                        })}

                </p>

                <p>

                    <strong>Despesas:</strong>

                    {" "}

                    {dados?.totalDespesas.toLocaleString("pt-BR",
                        {
                            style: "currency",
                            currency: "BRL"
                        })}

                </p>

                <p>

                    <strong>Saldo Líquido:</strong>

                    {" "}

                    {dados?.saldoLiquido.toLocaleString("pt-BR",
                        {
                            style: "currency",
                            currency: "BRL"
                        })}

                </p>

            </div>

        </div>

    );

}

export default Totais;