CREATE DATABASE myfinance

USE myfinance

CREATE TABLE planoconta(
	id IDENTITY NOT NULL,
	descricao VARCHAR(50) NOT NULL,
	tipo CHAR(1),
	PRIMARY KEY (id)
);

CREATE TABLE transacao(
	id int PRIMARY KEY IDENTITY NOT NULL,
	data datetime NOT NULL,
	valor DECIMAL(10,2) NOT NULL,
	historico TEXT,
	planocontaid INT NOT NULL
	FOREIGN KEY(planocontaid) REFERENCES planoconta(id)	
);

/* MASSA DE DADOS */
INSERT INTO planoconta(descricao, tipo) VALUES('Combustível', 'D')
INSERT INTO planoconta(descricao, tipo) VALUES('Alimentação', 'D')
INSERT INTO planoconta(descricao, tipo) VALUES('Plano de Saúde', 'D')
INSERT INTO planoconta(descricao, tipo) VALUES('IPTU', 'D')
INSERT INTO planoconta(descricao, tipo) VALUES('Salário', 'R')
INSERT INTO planoconta(descricao, tipo) VALUES('Dividendos de ações', 'R')

INSERT INTO transacao(data, valor, historico, planocontaid) VALUES('20230114 21:00', 800, 'Combustivel Renegade', 1)
INSERT INTO transacao(data, valor, historico, planocontaid) VALUES('20230314 21:20', 250, 'Rodizio na Chimarron mury', 2)
INSERT INTO transacao(data, valor, historico, planocontaid) VALUES('20230113 10:47', 1000, 'Unimed', 3)
INSERT INTO transacao(data, valor, historico, planocontaid) VALUES('20230112 11:00', 2000, 'IPTU Casa Friburgo', 4)
INSERT INTO transacao(data, valor, historico, planocontaid) VALUES('20230305 08:00', 100000, 'Salario', 5)
INSERT INTO transacao(data, valor, historico, planocontaid) VALUES('20230305 09:00', 500000, 'Dividendos', 6)


