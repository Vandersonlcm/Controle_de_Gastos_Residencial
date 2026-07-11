import { useEffect, useState } from "react";
import api from "../api/api";

/*
 Representa uma pessoa cadastrada.
*/
interface Pessoa {
    id: number;
    nome: string;
}

/*
 Representa uma transação retornada pela API.
*/
interface Transacao {
    id: number;
    descricao: string;
    valor: number;
    tipo: string;
    pessoaId: number;
    nomePessoa: string;
}

function Transacoes() {

    const [pessoas, setPessoas] = useState<Pessoa[]>([]);
    const [transacoes, setTransacoes] = useState<Transacao[]>([]);

    const [descricao, setDescricao] = useState("");
    const [valor, setValor] = useState(0);
    const [tipo, setTipo] = useState("Despesa");
    const [pessoaId, setPessoaId] = useState(0);

    /*
     Carrega as pessoas cadastradas.
    */
    async function carregarPessoas() {

        try {

            const response = await api.get("/Pessoas");

            setPessoas(response.data);

            if (response.data.length > 0) {
                setPessoaId(response.data[0].id);
            }

        }
        catch {

            alert("Erro ao carregar pessoas.");

        }

    }

    /*
     Carrega todas as transações.
    */
    async function carregarTransacoes() {

        try {

            const response = await api.get("/Transacoes");

            setTransacoes(response.data);

        }
        catch {

            alert("Erro ao carregar transações.");

        }

    }

    /*
     Cadastra uma nova transação.
    */
    async function cadastrarTransacao() {

        if (descricao.trim() === "") {

            alert("Informe a descrição.");

            return;

        }

        if (valor <= 0) {

            alert("Informe um valor maior que zero.");

            return;

        }

        try {

            await api.post("/Transacoes", {

                descricao,
                valor,
                tipo,
                pessoaId

            });

            setDescricao("");
            setValor(0);
            setTipo("Despesa");

            await carregarTransacoes();

            alert("Transação cadastrada com sucesso.");

        }
        catch (erro: any) {

            if (erro.response) {

                alert(erro.response.data);

            }
            else {

                alert("Erro ao conectar com a API.");

            }

        }

    }

    useEffect(() => {

        carregarPessoas();

        carregarTransacoes();

    }, []);

    return (

        <div className="card">

            <h2>Cadastro de Transações</h2>

            <input
                placeholder="Descrição"
                value={descricao}
                onChange={(e) => setDescricao(e.target.value)}
            />

            <input
                type="number"
                placeholder="Valor"
                value={valor}
                onChange={(e) => setValor(Number(e.target.value))}
            />

            <select
                value={tipo}
                onChange={(e) => setTipo(e.target.value)}
            >
                <option value="Despesa">Despesa</option>
                <option value="Receita">Receita</option>
            </select>

            <select
                value={pessoaId}
                onChange={(e) => setPessoaId(Number(e.target.value))}
            >

                {pessoas.map((pessoa) => (

                    <option
                        key={pessoa.id}
                        value={pessoa.id}
                    >
                        {pessoa.nome}
                    </option>

                ))}

            </select>

            <button onClick={cadastrarTransacao}>

                Cadastrar

            </button>

            <table>

                <thead>

                    <tr>

                        <th>ID</th>
                        <th>Descrição</th>
                        <th>Valor</th>
                        <th>Tipo</th>
                        <th>Pessoa</th>

                    </tr>

                </thead>

                <tbody>

                    {transacoes.map((t) => (

                        <tr key={t.id}>

                            <td>{t.id}</td>

                            <td>{t.descricao}</td>

                            <td>
                                {t.valor.toLocaleString("pt-BR", {
                                    style: "currency",
                                    currency: "BRL"
                                })}
                            </td>

                            <td>{t.tipo}</td>

                            <td>{t.nomePessoa}</td>

                        </tr>

                    ))}

                </tbody>

            </table>

        </div>

    );

}

export default Transacoes;