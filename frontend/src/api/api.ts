import axios from "axios";

/*
 Configuração da comunicação com a API.
 Todas as requisições utilizarão este endereço base.
*/
const api = axios.create({
    baseURL: "http://localhost:5282/api"
});

export default api;