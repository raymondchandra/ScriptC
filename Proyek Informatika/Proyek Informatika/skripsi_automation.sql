USE [skripsi_automation]
GO
/****** Object:  Table [dbo].[peran]    Script Date: 10/18/2013 17:48:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[peran](
	[id] [nchar](10) NOT NULL,
	[nama_peran] [varchar](30) NOT NULL,
 CONSTRAINT [PK_peran] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[jenis_skripsi]    Script Date: 10/18/2013 17:48:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[jenis_skripsi](
	[id] [nchar](10) NOT NULL,
	[nama_jenis] [varchar](30) NULL,
 CONSTRAINT [PK_jenis_skripsi] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[semester]    Script Date: 10/18/2013 17:48:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[semester](
	[id] [int] NOT NULL,
	[periode_semester] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ruang]    Script Date: 10/18/2013 17:48:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ruang](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nama_ruang] [varchar](10) NOT NULL,
 CONSTRAINT [PK_ruang] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[ruang] ON
INSERT [dbo].[ruang] ([id], [nama_ruang]) VALUES (1, N'asdf')
SET IDENTITY_INSERT [dbo].[ruang] OFF
/****** Object:  Table [dbo].[kategori_nilai]    Script Date: 10/18/2013 17:48:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[kategori_nilai](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[urutan] [int] NULL,
	[kategori] [varchar](50) NOT NULL,
	[bobot] [int] NOT NULL,
	[tipe] [varchar](10) NOT NULL,
	[jenis_skripsi_id] [nchar](10) NOT NULL,
 CONSTRAINT [PK_kategori_nilai] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[akun]    Script Date: 10/18/2013 17:48:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[akun](
	[username] [varchar](15) NOT NULL,
	[password] [varchar](15) NOT NULL,
	[aktif] [tinyint] NOT NULL,
	[last_login] [datetime] NULL,
	[peran] [nchar](10) NOT NULL,
 CONSTRAINT [account_pk] PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[jadwal_semester]    Script Date: 10/18/2013 17:48:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[jadwal_semester](
	[id] [int] NOT NULL,
	[tanggal] [datetime] NOT NULL,
	[isi] [varchar](1000) NOT NULL,
	[id_semester] [int] NOT NULL,
	[jenis_skripsi_id] [nchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[pengumuman]    Script Date: 10/18/2013 17:48:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pengumuman](
	[id] [int] NOT NULL,
	[tanggal] [datetime] NOT NULL,
	[isi] [varchar](5000) NOT NULL,
	[target] [nchar](10) NOT NULL,
 CONSTRAINT [pengumuman_pk] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[mahasiswa]    Script Date: 10/18/2013 17:48:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[mahasiswa](
	[NPM] [varchar](15) NOT NULL,
	[nama] [varchar](30) NOT NULL,
	[email] [varchar](30) NULL,
	[nomor_telepon] [varchar](15) NULL,
	[foto] [varchar](100) NULL,
	[username] [varchar](15) NOT NULL,
	[status] [varchar](100) NOT NULL,
 CONSTRAINT [mahasiswa_pk] PRIMARY KEY CLUSTERED 
(
	[NPM] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[dosen]    Script Date: 10/18/2013 17:48:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[dosen](
	[NIK] [varchar](15) NOT NULL,
	[nama] [varchar](30) NOT NULL,
	[username] [varchar](15) NOT NULL,
	[email] [varchar](30) NULL,
 CONSTRAINT [dosen_pk] PRIMARY KEY CLUSTERED 
(
	[NIK] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[calendar_event]    Script Date: 10/18/2013 17:48:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[calendar_event](
	[id] [int] NOT NULL,
	[text] [varchar](1000) NULL,
	[description] [varchar](1000) NULL,
	[start_date] [datetime] NOT NULL,
	[end_date] [datetime] NOT NULL,
	[username] [varchar](15) NOT NULL,
	[place] [varchar](50) NULL,
	[priority] [varchar](10) NULL,
	[type] [varchar](20) NULL,
 CONSTRAINT [calendar_event_pk_id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[pesanan_bimbingan]    Script Date: 10/18/2013 17:48:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pesanan_bimbingan](
	[id] [int] NOT NULL,
	[setuju] [int] NULL,
	[NIK_dosen] [varchar](15) NOT NULL,
	[NPM_mahasiswa] [varchar](15) NOT NULL,
	[tanggal_mulai] [datetime] NOT NULL,
	[tanggal_selesai] [datetime] NULL,
	[text] [varchar](1000) NULL,
	[description] [varchar](1000) NULL,
 CONSTRAINT [pk_id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[jadwal_kosong]    Script Date: 10/18/2013 17:48:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[jadwal_kosong](
	[id] [int] NOT NULL,
	[NIK_dosen] [varchar](15) NOT NULL,
	[NPM_mahasiswa] [varchar](15) NOT NULL,
	[tanggal_mulai] [datetime] NOT NULL,
	[tanggal_selesai] [datetime] NULL,
	[berulang] [int] NOT NULL,
	[text] [varchar](1000) NULL,
	[description] [varchar](1000) NULL,
 CONSTRAINT [jadwal_kosong_pk_id] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[topik]    Script Date: 10/18/2013 17:48:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[topik](
	[id] [int] NOT NULL,
	[judul] [varchar](1000) NOT NULL,
	[deskripsi] [varchar](1000) NULL,
	[keterangan] [varchar](50) NOT NULL,
	[NIK_pembimbing] [varchar](15) NOT NULL,
	[id_semester] [int] NOT NULL,
 CONSTRAINT [PK__topik__3213E83F1BFD2C07] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[skripsi]    Script Date: 10/18/2013 17:48:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[skripsi](
	[id] [int] NOT NULL,
	[jenis] [nchar](10) NOT NULL,
	[pengambilan_ke] [int] NOT NULL,
	[NIK_dosen_pembimbing] [varchar](15) NOT NULL,
	[NPM_mahasiswa] [varchar](15) NOT NULL,
	[id_semester_pengambilan] [int] NOT NULL,
	[id_topik] [int] NOT NULL,
	[nilai_akhir] [char](1) NULL,
 CONSTRAINT [skripsi_pk] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[sidang]    Script Date: 10/18/2013 17:48:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[sidang](
	[id] [int] NOT NULL,
	[tanggal] [datetime] NOT NULL,
	[ruang] [int] NULL,
	[id_skripsi] [int] NOT NULL,
 CONSTRAINT [sidang_pk] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[nilai]    Script Date: 10/18/2013 17:48:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[nilai](
	[id] [int] NOT NULL,
	[id_skripsi] [int] NOT NULL,
	[jenis] [varchar](10) NOT NULL,
	[angka] [float] NOT NULL,
	[bobot] [float] NULL,
	[NIK_pengisi] [varchar](15) NOT NULL,
	[isSidang] [int] NOT NULL,
 CONSTRAINT [nilai_pk] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[laporan]    Script Date: 10/18/2013 17:48:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[laporan](
	[id] [int] NOT NULL,
	[jenis] [varchar](10) NOT NULL,
	[deskripsi] [varchar](max) NULL,
	[tanggal_pengumpulan] [datetime] NOT NULL,
	[id_skripsi] [int] NOT NULL,
 CONSTRAINT [laporan_pk] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[bimbingan]    Script Date: 10/18/2013 17:48:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[bimbingan](
	[id] [int] NOT NULL,
	[isi] [varchar](100) NOT NULL,
	[deskripsi] [varchar](max) NULL,
	[id_skripsi] [int] NOT NULL,
	[tanggal_mulai] [datetime] NOT NULL,
	[tanggal_selesai] [datetime] NOT NULL,
 CONSTRAINT [bimbingan_pk] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[revisi]    Script Date: 10/18/2013 17:48:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[revisi](
	[id] [int] NOT NULL,
	[isi] [varchar](1000) NOT NULL,
	[id_sidang] [int] NOT NULL,
 CONSTRAINT [revisi_pk] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  ForeignKey [FK_akun_peran]    Script Date: 10/18/2013 17:48:41 ******/
ALTER TABLE [dbo].[akun]  WITH CHECK ADD  CONSTRAINT [FK_akun_peran] FOREIGN KEY([peran])
REFERENCES [dbo].[peran] ([id])
GO
ALTER TABLE [dbo].[akun] CHECK CONSTRAINT [FK_akun_peran]
GO
/****** Object:  ForeignKey [bimbingan_fk]    Script Date: 10/18/2013 17:48:41 ******/
ALTER TABLE [dbo].[bimbingan]  WITH CHECK ADD  CONSTRAINT [bimbingan_fk] FOREIGN KEY([id_skripsi])
REFERENCES [dbo].[skripsi] ([id])
GO
ALTER TABLE [dbo].[bimbingan] CHECK CONSTRAINT [bimbingan_fk]
GO
/****** Object:  ForeignKey [calendar_event_fk_username]    Script Date: 10/18/2013 17:48:41 ******/
ALTER TABLE [dbo].[calendar_event]  WITH CHECK ADD  CONSTRAINT [calendar_event_fk_username] FOREIGN KEY([username])
REFERENCES [dbo].[akun] ([username])
GO
ALTER TABLE [dbo].[calendar_event] CHECK CONSTRAINT [calendar_event_fk_username]
GO
/****** Object:  ForeignKey [dosen_fk]    Script Date: 10/18/2013 17:48:41 ******/
ALTER TABLE [dbo].[dosen]  WITH CHECK ADD  CONSTRAINT [dosen_fk] FOREIGN KEY([username])
REFERENCES [dbo].[akun] ([username])
GO
ALTER TABLE [dbo].[dosen] CHECK CONSTRAINT [dosen_fk]
GO
/****** Object:  ForeignKey [jadwal_kosong_fk_NIK]    Script Date: 10/18/2013 17:48:41 ******/
ALTER TABLE [dbo].[jadwal_kosong]  WITH CHECK ADD  CONSTRAINT [jadwal_kosong_fk_NIK] FOREIGN KEY([NIK_dosen])
REFERENCES [dbo].[dosen] ([NIK])
GO
ALTER TABLE [dbo].[jadwal_kosong] CHECK CONSTRAINT [jadwal_kosong_fk_NIK]
GO
/****** Object:  ForeignKey [jadwal_kosong_fk_NPM]    Script Date: 10/18/2013 17:48:41 ******/
ALTER TABLE [dbo].[jadwal_kosong]  WITH CHECK ADD  CONSTRAINT [jadwal_kosong_fk_NPM] FOREIGN KEY([NPM_mahasiswa])
REFERENCES [dbo].[mahasiswa] ([NPM])
GO
ALTER TABLE [dbo].[jadwal_kosong] CHECK CONSTRAINT [jadwal_kosong_fk_NPM]
GO
/****** Object:  ForeignKey [FK_jadwal_semester_jenis_skripsi]    Script Date: 10/18/2013 17:48:41 ******/
ALTER TABLE [dbo].[jadwal_semester]  WITH CHECK ADD  CONSTRAINT [FK_jadwal_semester_jenis_skripsi] FOREIGN KEY([jenis_skripsi_id])
REFERENCES [dbo].[jenis_skripsi] ([id])
GO
ALTER TABLE [dbo].[jadwal_semester] CHECK CONSTRAINT [FK_jadwal_semester_jenis_skripsi]
GO
/****** Object:  ForeignKey [fk_jadwal_semester_semester]    Script Date: 10/18/2013 17:48:41 ******/
ALTER TABLE [dbo].[jadwal_semester]  WITH CHECK ADD  CONSTRAINT [fk_jadwal_semester_semester] FOREIGN KEY([id_semester])
REFERENCES [dbo].[semester] ([id])
GO
ALTER TABLE [dbo].[jadwal_semester] CHECK CONSTRAINT [fk_jadwal_semester_semester]
GO
/****** Object:  ForeignKey [FK_kategori_nilai_jenis_skripsi]    Script Date: 10/18/2013 17:48:41 ******/
ALTER TABLE [dbo].[kategori_nilai]  WITH CHECK ADD  CONSTRAINT [FK_kategori_nilai_jenis_skripsi] FOREIGN KEY([jenis_skripsi_id])
REFERENCES [dbo].[jenis_skripsi] ([id])
GO
ALTER TABLE [dbo].[kategori_nilai] CHECK CONSTRAINT [FK_kategori_nilai_jenis_skripsi]
GO
/****** Object:  ForeignKey [laporan_fk]    Script Date: 10/18/2013 17:48:41 ******/
ALTER TABLE [dbo].[laporan]  WITH CHECK ADD  CONSTRAINT [laporan_fk] FOREIGN KEY([id_skripsi])
REFERENCES [dbo].[skripsi] ([id])
GO
ALTER TABLE [dbo].[laporan] CHECK CONSTRAINT [laporan_fk]
GO
/****** Object:  ForeignKey [mahasiswa_fk]    Script Date: 10/18/2013 17:48:41 ******/
ALTER TABLE [dbo].[mahasiswa]  WITH CHECK ADD  CONSTRAINT [mahasiswa_fk] FOREIGN KEY([username])
REFERENCES [dbo].[akun] ([username])
GO
ALTER TABLE [dbo].[mahasiswa] CHECK CONSTRAINT [mahasiswa_fk]
GO
/****** Object:  ForeignKey [nilai_fk_id_skripsi]    Script Date: 10/18/2013 17:48:41 ******/
ALTER TABLE [dbo].[nilai]  WITH CHECK ADD  CONSTRAINT [nilai_fk_id_skripsi] FOREIGN KEY([id_skripsi])
REFERENCES [dbo].[skripsi] ([id])
GO
ALTER TABLE [dbo].[nilai] CHECK CONSTRAINT [nilai_fk_id_skripsi]
GO
/****** Object:  ForeignKey [nilai_fk_NIK_pengisi]    Script Date: 10/18/2013 17:48:41 ******/
ALTER TABLE [dbo].[nilai]  WITH CHECK ADD  CONSTRAINT [nilai_fk_NIK_pengisi] FOREIGN KEY([NIK_pengisi])
REFERENCES [dbo].[dosen] ([NIK])
GO
ALTER TABLE [dbo].[nilai] CHECK CONSTRAINT [nilai_fk_NIK_pengisi]
GO
/****** Object:  ForeignKey [FK_pengumuman_peran]    Script Date: 10/18/2013 17:48:41 ******/
ALTER TABLE [dbo].[pengumuman]  WITH CHECK ADD  CONSTRAINT [FK_pengumuman_peran] FOREIGN KEY([target])
REFERENCES [dbo].[peran] ([id])
GO
ALTER TABLE [dbo].[pengumuman] CHECK CONSTRAINT [FK_pengumuman_peran]
GO
/****** Object:  ForeignKey [fk_dosen]    Script Date: 10/18/2013 17:48:41 ******/
ALTER TABLE [dbo].[pesanan_bimbingan]  WITH CHECK ADD  CONSTRAINT [fk_dosen] FOREIGN KEY([NIK_dosen])
REFERENCES [dbo].[dosen] ([NIK])
GO
ALTER TABLE [dbo].[pesanan_bimbingan] CHECK CONSTRAINT [fk_dosen]
GO
/****** Object:  ForeignKey [fk_mahasiswa]    Script Date: 10/18/2013 17:48:41 ******/
ALTER TABLE [dbo].[pesanan_bimbingan]  WITH CHECK ADD  CONSTRAINT [fk_mahasiswa] FOREIGN KEY([NPM_mahasiswa])
REFERENCES [dbo].[mahasiswa] ([NPM])
GO
ALTER TABLE [dbo].[pesanan_bimbingan] CHECK CONSTRAINT [fk_mahasiswa]
GO
/****** Object:  ForeignKey [revisi_fk]    Script Date: 10/18/2013 17:48:41 ******/
ALTER TABLE [dbo].[revisi]  WITH CHECK ADD  CONSTRAINT [revisi_fk] FOREIGN KEY([id_sidang])
REFERENCES [dbo].[sidang] ([id])
GO
ALTER TABLE [dbo].[revisi] CHECK CONSTRAINT [revisi_fk]
GO
/****** Object:  ForeignKey [FK_sidang_ruang]    Script Date: 10/18/2013 17:48:41 ******/
ALTER TABLE [dbo].[sidang]  WITH CHECK ADD  CONSTRAINT [FK_sidang_ruang] FOREIGN KEY([ruang])
REFERENCES [dbo].[ruang] ([id])
GO
ALTER TABLE [dbo].[sidang] CHECK CONSTRAINT [FK_sidang_ruang]
GO
/****** Object:  ForeignKey [sidang_fk_id_skripsi]    Script Date: 10/18/2013 17:48:41 ******/
ALTER TABLE [dbo].[sidang]  WITH CHECK ADD  CONSTRAINT [sidang_fk_id_skripsi] FOREIGN KEY([id_skripsi])
REFERENCES [dbo].[skripsi] ([id])
GO
ALTER TABLE [dbo].[sidang] CHECK CONSTRAINT [sidang_fk_id_skripsi]
GO
/****** Object:  ForeignKey [FK_skripsi_jenis_skripsi]    Script Date: 10/18/2013 17:48:41 ******/
ALTER TABLE [dbo].[skripsi]  WITH CHECK ADD  CONSTRAINT [FK_skripsi_jenis_skripsi] FOREIGN KEY([jenis])
REFERENCES [dbo].[jenis_skripsi] ([id])
GO
ALTER TABLE [dbo].[skripsi] CHECK CONSTRAINT [FK_skripsi_jenis_skripsi]
GO
/****** Object:  ForeignKey [fk_skripsi_semester]    Script Date: 10/18/2013 17:48:41 ******/
ALTER TABLE [dbo].[skripsi]  WITH CHECK ADD  CONSTRAINT [fk_skripsi_semester] FOREIGN KEY([id_semester_pengambilan])
REFERENCES [dbo].[semester] ([id])
GO
ALTER TABLE [dbo].[skripsi] CHECK CONSTRAINT [fk_skripsi_semester]
GO
/****** Object:  ForeignKey [fk_skripsi_topik]    Script Date: 10/18/2013 17:48:41 ******/
ALTER TABLE [dbo].[skripsi]  WITH CHECK ADD  CONSTRAINT [fk_skripsi_topik] FOREIGN KEY([id_topik])
REFERENCES [dbo].[topik] ([id])
GO
ALTER TABLE [dbo].[skripsi] CHECK CONSTRAINT [fk_skripsi_topik]
GO
/****** Object:  ForeignKey [skripsi_fk]    Script Date: 10/18/2013 17:48:41 ******/
ALTER TABLE [dbo].[skripsi]  WITH CHECK ADD  CONSTRAINT [skripsi_fk] FOREIGN KEY([NIK_dosen_pembimbing])
REFERENCES [dbo].[dosen] ([NIK])
GO
ALTER TABLE [dbo].[skripsi] CHECK CONSTRAINT [skripsi_fk]
GO
/****** Object:  ForeignKey [skripsi_fk2]    Script Date: 10/18/2013 17:48:41 ******/
ALTER TABLE [dbo].[skripsi]  WITH CHECK ADD  CONSTRAINT [skripsi_fk2] FOREIGN KEY([NPM_mahasiswa])
REFERENCES [dbo].[mahasiswa] ([NPM])
GO
ALTER TABLE [dbo].[skripsi] CHECK CONSTRAINT [skripsi_fk2]
GO
/****** Object:  ForeignKey [fk_topik_dosen]    Script Date: 10/18/2013 17:48:41 ******/
ALTER TABLE [dbo].[topik]  WITH CHECK ADD  CONSTRAINT [fk_topik_dosen] FOREIGN KEY([NIK_pembimbing])
REFERENCES [dbo].[dosen] ([NIK])
GO
ALTER TABLE [dbo].[topik] CHECK CONSTRAINT [fk_topik_dosen]
GO
/****** Object:  ForeignKey [fk_topik_semsester]    Script Date: 10/18/2013 17:48:41 ******/
ALTER TABLE [dbo].[topik]  WITH CHECK ADD  CONSTRAINT [fk_topik_semsester] FOREIGN KEY([id_semester])
REFERENCES [dbo].[semester] ([id])
GO
ALTER TABLE [dbo].[topik] CHECK CONSTRAINT [fk_topik_semsester]
GO
