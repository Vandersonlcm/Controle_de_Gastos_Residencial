import "./styles/App.css";

import Pessoas from "./components/Pessoas";
import Transacoes from "./components/Transacoes";
import Totais from "./components/Totais";

function App() {

    return (

        <div className="container">

            <header>

                <h1>

                    Controle de Gastos Residenciais

                </h1>

                <p>

                    Sistema desenvolvido em React + TypeScript consumindo uma API ASP.NET Core.

                </p>

            </header>

            <Pessoas />

            <Transacoes />

            <Totais />

        </div>

    );

}

export default App;