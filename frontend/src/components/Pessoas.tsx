import { useEffect, useState } from "react";
import api from "../api/api";

/*
 Interface utilizada para representar uma pessoa.
*/
interface Pessoa {
    id: number;
    nome: string;
    idade: number;
}

function Pessoas() {

    // Lista de pessoas cadastradas.
    const [pessoas, setPessoas] = useState<Pessoa[]>([]);

    // Campos do formulário.
    const [nome, setNome] = useState("");
    const [idade, setIdade] = useState(0);

    /*
     Carrega todas as pessoas cadastradas.
    */
    async function carregarPessoas() {

        try {

            const response = await api.get("/Pessoas");

            setPessoas(response.data);

        } catch {

            alert("Erro ao carregar pessoas.");

        }

    }

    /*
     Cadastra uma nova pessoa.
    */
    async function cadastrarPessoa() {

        if (nome.trim() === "") {

            alert("Informe o nome.");

            return;

        }

        try {

            await api.post("/Pessoas", {

                nome,
                idade

            });

            setNome("");
            setIdade(0);

            carregarPessoas();

            alert("Pessoa cadastrada com sucesso.");

        }
        catch {

            alert("Erro ao cadastrar pessoa.");

        }

    }

    /*
     Exclui uma pessoa.
    */
    async function excluirPessoa(id: number) {

        if (!window.confirm("Deseja excluir esta pessoa?"))
            return;

        try {

            await api.delete(`/Pessoas/${id}`);

            carregarPessoas();

            alert("Pessoa excluída com sucesso.");

        }
        catch {

            alert("Erro ao excluir pessoa.");

        }

    }

    /*
     Executado quando o componente é carregado.
    */
    useEffect(() => {

        carregarPessoas();

    }, []);

    return (

        <div className="card">

            <h2>Cadastro de Pessoas</h2>

            <input
                placeholder="Nome"
                value={nome}
                onChange={(e) => setNome(e.target.value)}
            />

            <input
                type="number"
                placeholder="Idade"
                value={idade}
                onChange={(e) => setIdade(Number(e.target.value))}
            />

            <button onClick={cadastrarPessoa}>
                Cadastrar
            </button>

            <table>

                <thead>

                    <tr>

                        <th>ID</th>
                        <th>Nome</th>
                        <th>Idade</th>
                        <th>Ações</th>

                    </tr>

                </thead>

                <tbody>

                    {pessoas.map((pessoa) => (

                        <tr key={pessoa.id}>

                            <td>{pessoa.id}</td>

                            <td>{pessoa.nome}</td>

                            <td>{pessoa.idade}</td>

                            <td>

                                <button
                                    onClick={() => excluirPessoa(pessoa.id)}
                                >
                                    Excluir
                                </button>

                            </td>

                        </tr>

                    ))}

                </tbody>

            </table>

        </div>

    );

}

export default Pessoas;