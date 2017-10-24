import pyodbc 
cnxn = pyodbc.connect("Driver={SQL Server Native Client 11.0};"
                      "Server=FORLOGIC565;"
                      "Database=consultorio;"
                      "Trusted_Connection=yes;")



cursor = cnxn.cursor()
#cursor.execute('SELECT DISTINCT pg.id, pg.id_plano_tratamento, pg.montante_procedimentos_contratados, pg.montante_final, pg.removido/*, pg.data_aprovacao*/ FROM planos_pagamentos pg LEFT JOIN planos_tratamentos pt ON (pt.id = pg.id_plano_tratamento) LEFT JOIN unidades_clientes uc ON (uc.id = pt.id_unidade_cliente) LEFT JOIN unidades u ON (u.id = uc.id_unidade) WHERE u.id in (2,8)')
cursor.execute('SELECT c.observacao FROM clientes c WHERE c.id_unidade_padrao in (2,8)')
#CONVERT(CHAR(23),CONVERT(DATETIME,'7/19/2013',101),121);
with open('clientes.csv', 'w') as file:
	for row in cursor:
		#if "datetime" in str(row):
			#file.write(str(row)+'\n')
		#else:
			file.write(str(row).replace(')','').replace('(','')+'\n')
		#print('row = %r' % (row,))
	file.close()

print('ACABOU')