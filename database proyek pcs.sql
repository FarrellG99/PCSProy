DROP TABLE DOSEN CASCADE CONSTRAINT PURGE;
DROP TABLE MAHASISWA CASCADE CONSTRAINT PURGE;
DROP TABLE JURUSAN CASCADE CONSTRAINT PURGE;
DROP TABLE MATAKULIAH CASCADE CONSTRAINT PURGE;
DROP TABLE KELAS CASCADE CONSTRAINT PURGE;
DROP TABLE PERIODE CASCADE CONSTRAINT PURGE;
DROP TABLE PERWALIAN CASCADE CONSTRAINT PURGE;
DROP TABLE DPERWALIAN CASCADE CONSTRAINT PURGE;
DROP TABLE RUANGAN CASCADE CONSTRAINT PURGE;
DROP TABLE JADWAL CASCADE CONSTRAINT PURGE;

CREATE TABLE DOSEN (
	NIP	VARCHAR2(9) PRIMARY KEY,
	NAMA_DOSEN	VARCHAR2(50) NOT NULL,
	JKDOSEN	VARCHAR2(1) NOT NULL,
	AGAMA_DOSEN	VARCHAR2(15),
	TEMPATLHR_DOSEN	VARCHAR2(30) NOT NULL,
	TANGGAL_DOSEN DATE NOT NULL,
	ALAMAT_DOSEN VARCHAR2(50) NOT NULL,
	STATUS_WALI VARCHAR2(1) NOT NULL CHECK(STATUS_WALI = 0 OR STATUS_WALI = 0),
	NOTELP_DOSEN VARCHAR2(15) NOT NULL 
);
CREATE TABLE MAHASISWA (
	NRP	VARCHAR2(9) PRIMARY KEY,
	NIP	VARCHAR2(9) REFERENCES DOSEN(NIP),
	NAMA_MAHASISWA	VARCHAR2(50) NOT NULL,
	AGAMA_MAHASISWA	VARCHAR2(15),
	TEMPATLAHIR_MHS	VARCHAR2(30) NOT NULL,
	TGLLAHIR_MHS	DATE NOT NULL,
	ALAMAT_MHS	VARCHAR2(50) NOT NULL,
	NOTELP_MHS	VARCHAR2(15) NOT NULL,
	NAMAORTU_MHS	VARCHAR2(50) NOT NULL,
	NOTELPORTU_MHS	VARCHAR2(15) NOT NULL
);
CREATE TABLE JURUSAN (
	KODE_JURUSAN VARCHAR2(3) PRIMARY KEY,
	NAMA_JURUSAN VARCHAR2(30) NOT NULL,
	NIP	VARCHAR2(9) REFERENCES DOSEN(NIP)
);

CREATE TABLE MATAKULIAH (
	KODE_MK	VARCHAR2(5)	PRIMARY KEY,
	KODE_JURUSAN VARCHAR2(2) REFERENCES JURUSAN(KODE_JURUSAN),
	NAMA_MK	VARCHAR2(50) NOT NULL,
	JUMLAH_SKS	NUMBER(1) NOT NULL,
	STATUS	NUMBER(1) NOT NULL CHECK(STATUS = 0 OR STATUS = 1),
	THNAJARAN_MK VARCHAR2(4) NOT NULL
);

CREATE TABLE KELAS (
	KODE_KELAS	VARCHAR2(5)	PRIMARY KEY,
	KODE_MK	VARCHAR2(3)	REFERENCES MATAKULIAH(KODE_MK),
	NAMA_KELAS	VARCHAR2(50) NOT NULL
);

CREATE TABLE PERIODE (
	KODE_THN_AJARAN	VARCHAR2(5)	PRIMARY KEY,
	TAHUN_AJARAN VARCHAR2(30)	
);

CREATE TABLE PERWALIAN (
	KODE_FRS	VARCHAR2(8)	PRIMARY KEY,
	NRP	VARCHAR2(9)	REFERENCES MAHASISWA(NRP),
	KODE_THN_AJARAN	VARCHAR2(5)	REFERENCES PERIODE(KODE_THN_AJARAN),
	IPS	NUMBER(5) NOT NULL
);

CREATE TABLE DPERWALIAN (
	KODE_FRS VARCHAR2(8)	REFERENCES PERWALIAN(KODE_FRS),
	KODE_MK	VARCHAR2(5)	REFERENCES MATAKULIAH(KODE_MK),
	STATUS_PERWALIAN NUMBER(1) NOT NULL,
	TANGGAL_PERWALIAN DATE,
	PENGAMBILAN_KE NUMBER(1),
	PRIMARY KEY (KODE_FRS,KODE_MK)
);

CREATE TABLE RUANGAN (
	KODE_RUANGAN VARCHAR2(5) PRIMARY KEY,
	JUMLAH_KURSI NUMBER(3),
	PERUNTUKAN VARCHAR2(30) NOT NULL
);

CREATE TABLE JADWAL (
	KODE_JADWAL	VARCHAR2(5)	PRIMARY KEY,
	NIP	VARCHAR2(9) REFERENCES DOSEN(NIP),
	KODE_RUANGAN VARCHAR2(5) REFERENCES RUANGAN(KODE_RUANGAN),
	KODE_MK VARCHAR2(5)	REFERENCES MATAKULIAH(KODE_MK),
	KODE_KELAS VARCHAR2(3) REFERENCES KELAS(KODE_KELAS),
	KODE_THN_AJARAN	VARCHAR2(5)	REFERENCES PERIODE(KODE_THN_AJARAN),
	HARI VARCHAR2(10) NOT NULL,
	JAM_AWAL VARCHAR2(5) NOT NULL,
	JAM_AKHIR VARCHAR2(5)NOT NULL
);

INSERT INTO DOSEN VALUES('AD001','Agatha Dinarah Sri Rumestri, S.T.',�P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Purwodadi 1 No.180',�0�, '(807) 948-6503');
INSERT INTO DOSEN VALUES('AG001','Agus Gunawan, Ir., MSEE.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Raya Menur No.109', �0�, '(554) 492-8462');
INSERT INTO DOSEN VALUES('AD002','Alan David Prayogi, S.T.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Pandugo No.133', �0�,'(911) 173-7286');
INSERT INTO DOSEN VALUES('DA001','David Alexandre, S.Kom., MBA', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Raya Kali Rungkut No.35', �0�,'(550) 985-4889');
INSERT INTO DOSEN VALUES('AA001','Amelia Agustina, S.Ds.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Nginden 4C No.92', �0�,'(491) 740-5799');
INSERT INTO DOSEN VALUES('AA002','Amelia Alexandra, S.Kom.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Raya Lontar Candi Lempung No.165', �0�,'(813) 354-9015');
INSERT INTO DOSEN VALUES('AS001','Andri Suhartono, S.T.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Rungkut Asri Utara XX No.98', �0�,'(854) 777-6830');
INSERT INTO DOSEN VALUES('AT001','Arya Tandy Hermawan, Ir., M.T.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Pregolan Bunder No.79', �0�,'(978) 662-6163');
INSERT INTO DOSEN VALUES('AA003','Audrey Ayu Dianaris, S.SI.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Pucang Kerep No.38', �0�,'(972) 511-7089');
INSERT INTO DOSEN VALUES('BB001','Bonifacia Bulan Arumingtyas, S.Ds.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Raya Gunung Anyar Tambak No.177', �0�, '(806) 692-1898');
INSERT INTO DOSEN VALUES('BS001','Budhy Sutanto, Ir.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Raya Darmo No.179', �0�, '(601) 437-6256');
INSERT INTO DOSEN VALUES('CA001','Christian Aditya, S.Kom.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Ngagel Madya 5 No.47', �0�, '(215) 664-0788');
INSERT INTO DOSEN VALUES('DA002','Decky Avrilukito, S.Sn., M.M.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Raya Kali Rungkut 5 No.194', �0�, '(839) 116-5238');
INSERT INTO DOSEN VALUES('DC001','Detyo Campoko, S.T.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Raya Jemur Andayani No.164', �0�, '(387) 150-2839');
INSERT INTO DOSEN VALUES('DD001','Devi Dwi Purwanto, S.Kom., M.Kom.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Raya Darmo No.228', �0�, '(338) 565-2101');
INSERT INTO DOSEN VALUES('ER001','Eka Rahayu Setyaningsih, S.Kom., M.Kom.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Nginden 5 No.30', �0�,'(387) 886-0227');
INSERT INTO DOSEN VALUES('ES001','Endang Setyati, Dr., Ir., Hj.,, M.T.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Panglima Sudirman No.44', �0�,'(440) 259-6334');
INSERT INTO DOSEN VALUES('EP001','Edwin Pramana, Ir., M.AppSc.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Rajawali No.115', �0�, '(789) 781-2429');
INSERT INTO DOSEN VALUES('EP002','Erick Pranata, S.Kom., M.Kom.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Nyamplungan No.127', �0�, '(900) 722-3063');
INSERT INTO DOSEN VALUES('ES002','Eric Sugiharto, S.SI.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Pahlawan No.70', �0�,'(543) 188-1693');
INSERT INTO DOSEN VALUES('EI001','Esther Irawati Setiawan, S.Kom, M.Kom', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Nias No.111', �0�, '(970) 770-6610');
INSERT INTO DOSEN VALUES('FX001','F.X. Ferdinandus, Ir., M.T.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Nginden 1 No.184', �0�,'(715) 409-4037');
INSERT INTO DOSEN VALUES('FH001','Francisca Haryanti Chandra, Dr., Ir., M.T.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Raya Gubeng No.91', �0�, '(163) 337-4634');
INSERT INTO DOSEN VALUES('GL001','Grace Levina Dewi, S.Kom., M.Kom.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Raya Medokan Ayu. No.242', �0�,'(495) 516-3297');
INSERT INTO DOSEN VALUES('GU001','Gunawan, Ir., M.Kom.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Pasar Turi No.7', �0�, '(461) 772-6192');
INSERT INTO DOSEN VALUES('HS001','Hari Sutiksno, Dr., Ir., M.T.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Nyamplungan 240 No.253', �0�, '(306) 386-2385');
INSERT INTO DOSEN VALUES('HJ001','Hartarto Junaedi, S.Kom. ,M.Kom.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Raya Prapen No.236', �0�,'(168) 512-1638');
INSERT INTO DOSEN VALUES('HA001','Hendrawan Armanto, S.Kom., M.Kom.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Nginden Intan Timur 1 No.72', �0�, '(749) 688-1844');
INSERT INTO DOSEN VALUES('HB001','Herman Budianto, Ir., M.M.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Raya Jemur Handayani 50 No.124', �0�, '(375) 452-8845');
INSERT INTO DOSEN VALUES('IC001','Iwan Chandra, S.Kom.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Nginden Semolo No.59', �0�, '(241) 989-7062');
INSERT INTO DOSEN VALUES('IG001','Imelda Gozali, B.Eng., M.Pd.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Raya Manyar No.171', �0�,'(989) 895-5803');
INSERT INTO DOSEN VALUES('IL001','Indra Lesmana, S.Kom.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Raya Ngagel No.243', �0�, '(639) 449-9313');
INSERT INTO DOSEN VALUES('IE001','Ivan Eliata Kusuma, S.T., M.T.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Raya Rungkut Madya No.88', �0�, '(685) 567-0093');
INSERT INTO DOSEN VALUES('JC001','Jacky Cahyadi, S.Sn.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Raya Jemursari No.158', �0�,'(414) 293-4960');
INSERT INTO DOSEN VALUES('JP001','Jaya Pranata, S.Kom., M.T.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Pasar Kembang 4-6 No.201', �0�,'(244) 654-6725');
INSERT INTO DOSEN VALUES('JN001','Dr. Jenny Ngo, MSc.Ed.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Rungkut Asri Barat IX No.224', �0�,'(852) 933-8611');
INSERT INTO DOSEN VALUES('JS001','Joan Santoso, S.Kom., M.Kom.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Raya Kendangsari No.127', �0�, '(204) 949-4525');
INSERT INTO DOSEN VALUES('JH001','Jonie Hermanto, S.Kom.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Pucang Sewu VII No.139', �0�,'(648) 520-7444');
INSERT INTO DOSEN VALUES('JP002','Judi Prajetno Sugiono, Ir., M.M.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Ratna No.15', �0�, '(441) 275-1723');
INSERT INTO DOSEN VALUES('KE001','Kelvin, S.T., M.M.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Raya ITS, Surabaya No.125', �0�,'(360) 874-2185');
INSERT INTO DOSEN VALUES('KS001','Kevin Setiono, S.Kom.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Pemuda 33-35 No.118', �0�,'(448) 666-0337');
INSERT INTO DOSEN VALUES('KG001','Khinardi Gunawan, Ir.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Raya Jambangan No.19', �0�,'(451) 233-4528');
INSERT INTO DOSEN VALUES('KI001','Kristian Indradiarta G., S.Kom.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'jalan pajajaran No.200', �0�,'(393) 181-2709');
INSERT INTO DOSEN VALUES('LZ001','Lukman Zaman P. C. S. W., S.Kom., M.Kom.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Penjernihan No.129', �0�,'(315) 672-8258');
INSERT INTO DOSEN VALUES('IY001','Is Yuniarto Nafawi, S.Sn.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Ronggolawe No.13', �0�,'(423) 126-1901');
INSERT INTO DOSEN VALUES('MB001','Martinus Brahma Dwi Laksana, S.T.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'jalan raya jemur andayani No.4', �0�, '(137) 910-9149');
INSERT INTO DOSEN VALUES('IM001','Indra Maryati, S.Kom., M.Kom.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Raya Jemursari No.247', �0�, '(693) 786-9557');
INSERT INTO DOSEN VALUES('MS001','Mikhael setiawan, S.Kom.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Rangkah gg 7 no 26-B No.173', �0�, '(827) 883-1604');
INSERT INTO DOSEN VALUES('OB001','Oswald Baskoro Satyoadi, Ir., M.Kom.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Pengampon VI No.28', �0�,'(818) 120-3834');
INSERT INTO DOSEN VALUES('CP001','C. Pickerling S.Kom.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Rungkut Asri Tengah VIII No.24', �0�, '(720) 119-2929');
INSERT INTO DOSEN VALUES('PE001','Pram Eliyah Yuliana, S.T., M.T.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Pucang Anom Timur No.161', �0�,'(400) 162-3791');
INSERT INTO DOSEN VALUES('SR001','Sri Rahayu, S.T., M.T.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Raya Rungkut Madya Kav 8-10 No.227', �0�, '(647) 961-7160');
INSERT INTO DOSEN VALUES('RA001','Reddy Alexandro Harianto, S.Kom., M. Kom', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Raya Ngagel No.158', �0�, '(245) 571-3018');
INSERT INTO DOSEN VALUES('RL001','Riandika Lumaris, S. Kom., M.Kom.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'jalan ngagel jaya utara No.250', �0�,'(785) 218-4740');
INSERT INTO DOSEN VALUES('SA001','Sandy Ardianto, S.Kom.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Pesapen Selatan No.251', �0�,'(390) 718-1365');
INSERT INTO DOSEN VALUES('SA002','Setya Ardhi, S.T., M.Kom.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Pesapen Kali No.230', �0�, '(796) 949-0679');
INSERT INTO DOSEN VALUES('SU001','Sufiana, Dra., M.Sn.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Raya Jemur Sari No.82', �0�, '(469) 402-9819');
INSERT INTO DOSEN VALUES('ST001','S. Tigor B. Tambunan, S.T., M.M.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Nginden Intan Barat No.199', �0�,'(228) 394-7066');
INSERT INTO DOSEN VALUES('ST002','Suhatati Tjandra, Ir., M.Kom.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Pasar Turi 1 No.221', �0�,'(831) 323-4845');
INSERT INTO DOSEN VALUES('HT001','Herman Thuan To Saurik, S.Kom., M.T.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Raya Prapen No.202', �0�,'(282) 959-2064');
INSERT INTO DOSEN VALUES('TP001','Tjwanda Putera Gunawan, Ir., M.Pd.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'jalan pogot No.44', �0�,'(562) 987-4956');
INSERT INTO DOSEN VALUES('MD001','Masrara Dwi Yanti Handayani, S.Pd.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Panglima Sudirman No.210', �0�, '(993) 585-4544');
INSERT INTO DOSEN VALUES('YM001','Yuliana Melita Pranoto, S.Kom., M.Kom.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Raya Lontar No.237', �0�, '(780) 149-0147');
INSERT INTO DOSEN VALUES('YJ001','Yohanes Joko H., Drs., M.S.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Raya Lontar No.66', �0�,'(135) 954-0485');
INSERT INTO DOSEN VALUES('YK001','Yosi Kristian, S.Kom., M.Kom.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE,'Jalan Nginden II No.88', �0�, '(918) 883-8701');
INSERT INTO DOSEN VALUES('YW001','Yulius Widi Nugroho, S.Sn. ,M.Si.', �P�,�KRISTEN�,�SURABAYA�,SYSDATE, 'Jalan Nginden 2 No.99', �0�,'(384) 771-7662');

INSERT INTO MAHASISWA VALUES('217011670','EI001','Daud Kurnia Tanamas','kristen','surabaya',sysdate,'surabaya','081-123','ferguso','081-321');
INSERT INTO MAHASISWA VALUES('217011671','EI001','Dimas Bagus Nugroho Verdy Anwar','kristen','surabaya',sysdate,'surabaya','081-123','ferguso','081-321');
INSERT INTO MAHASISWA VALUES('217011672','EI001','Hafizh Ainanto Pratama','kristen','surabaya',sysdate,'surabaya','081-123','ferguso','081-321');
INSERT INTO MAHASISWA VALUES('217011673','EI001','Laurentius Dirga Cahya Putra','kristen','surabaya',sysdate,'surabaya','081-123','ferguso','081-321');
INSERT INTO MAHASISWA VALUES('217011674','EI001','Reyhan Rahma Sugiarto','kristen','surabaya',sysdate,'surabaya','081-123','ferguso','081-321');
INSERT INTO MAHASISWA VALUES('217011675','EI001','Rifqy Alfiansyah','kristen','surabaya',sysdate,'surabaya','081-123','ferguso','081-321');
INSERT INTO MAHASISWA VALUES('217102588','HS001','Andreas Wijaya','kristen','surabaya',sysdate,'surabaya','081-123','ferguso','081-321');
INSERT INTO MAHASISWA VALUES('217102589','HS001','Christine Calista','kristen','surabaya',sysdate,'surabaya','081-123','ferguso','081-321');
INSERT INTO MAHASISWA VALUES('217102590','HS001','Dafi Gumawang Priadi','kristen','surabaya',sysdate,'surabaya','081-123','ferguso','081-321');
INSERT INTO MAHASISWA VALUES('217102591','HS001','Jeffry Sandy Purnomo','kristen','surabaya',sysdate,'surabaya','081-123','ferguso','081-321');
INSERT INTO MAHASISWA VALUES('217102592','SA002','Rio Ardandi','kristen','surabaya',sysdate,'surabaya','081-123','ferguso','081-321');
INSERT INTO MAHASISWA VALUES('217102593','SA002','Rohman Is Wahyudi','kristen','surabaya',sysdate,'surabaya','081-123','ferguso','081-321');
INSERT INTO MAHASISWA VALUES('217102594','SA002','Teguh Santoso','kristen','surabaya',sysdate,'surabaya','081-123','ferguso','081-321');
INSERT INTO MAHASISWA VALUES('217102595','SA002','Tonny Salim Suswanto','kristen','surabaya',sysdate,'surabaya','081-123','ferguso','081-321');

INSERT INTO JURUSAN VALUES('11','S1-Teknik Informatika','YK001');
INSERT INTO JURUSAN VALUES('17','S1-Desain Komunikasi Visual','LZ001');
INSERT INTO JURUSAN VALUES('18','S1-Sistem Informasi Bisnis','HJ001');
INSERT INTO JURUSAN VALUES('10','S1-Teknik Elektro','GU001');
INSERT INTO JURUSAN VALUES('12','S1-Teknik Industri','ST001');
INSERT INTO JURUSAN VALUES('14','S1-Desain Produk','AD001');
INSERT INTO JURUSAN VALUES('01','D3-Manajemen Informatika','YK001');
INSERT INTO JURUSAN VALUES('31','Prog.Prof.S1-Teknik Informatika','EI001');
INSERT INTO JURUSAN VALUES('21','S2-Teknologi Informasi','AT001');

create or replace function autogennip
(inputnama in varchar2)
return varchar2
is
	namadepan varchar2(1);
	namabelakang varchar2(1);
	gabung varchar2(2);
	angka number(3);
	hasil varchar2(5);
begin
	if instr(inputnama,' ',1) = 0 then
		namadepan := initcap(substr(inputnama,1,1));
		namabelakang := initcap(substr(inputnama,2,1));
	elsif instr(inputnama,' ',-1,1) > 0 then
		namadepan := initcap(substr(inputnama,1,1));
		namabelakang := initcap(substr(inputnama,instr(inputnama, ' ',-1,1)+1,1));
	elsif instr(inputnama,' ',1) > 0 then
		namadepan := initcap(substr(inputnama,1,1));
		namabelakang := initcap(substr(inputnama,instr(inputnama, ' ',1)+1,1));
	end if;
	gabung := namadepan || namabelakang;
	select to_number(max(substr(nip,3,3))) into angka from dosen where substr(nip,1,2) = gabung;
	if angka is NULL then
		hasil := gabung || lpad(1,3,'0');
	else
		if angka > 0 then
			hasil := gabung || lpad(angka+1,3,'0');
		else
			hasil := gabung || lpad(angka,3,'0');
		end if;
	end if;
	return hasil;
end;
/
show err;
select autogennip('Farrell Gunawan') from dual;

set serveroutput on;
create or replace procedure p1
is
begin
	dbms_output.put_line('========================');
	dbms_output.put_line('');
end;
/
show err;
execute p1;