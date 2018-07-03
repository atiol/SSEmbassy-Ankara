INSERT INTO [embassy_db].[dbo].[personel]([personel_id], [position_id], [name], [surname], [telefon], [fax], [contract_start], [contract_end], [img_url], [gender])
VALUES(
	'S-002',
	4,
	'ACHAI BULABEK CHOL',
	'PIOK',
	'+90 312 4360285',
	'+90 312 4360284',
	'2018-06-29',
	'2020-06-29',
	'C:\\Users\\Atiol Beato\\Desktop\\ssankara_v2\\img\\achai.jpg',
	'F'
);

select * from personel
update [personel]
set [gender] = 'M'
where [personel_id] = 'S-001'

select * from positions