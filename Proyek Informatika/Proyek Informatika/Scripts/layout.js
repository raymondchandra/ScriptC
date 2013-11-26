function onSelectMenuLayout(e) {
    var item = $(e.item);
    // ======== Home ======= //
    if (item.find('> .t-link').text() == "Home") {
        location.reload();
    } else if (item.find('> .t-link').text() == "Topik") {
        $('#content').load('Home/Topik');
    } else if (item.find('> .t-link').text() == "About") {
        $('#content').load('Home/About');
    }
}

function onSelectMenuMahasiswa(e) {
    var item = $(e.item);
    // ======== Home ======= //
    if (item.find('> .t-link').text() == "Pengumuman") {
        $('#content').load('Pengumuman/Pengumuman');
    } else if (item.find('> .t-link').text() == "Biodata") {    
        $('#content').load('Profile/Biodata');
    } else if (item.find('> .t-link').text() == "Sejarah") {
        $('#content').load('Profile/Sejarah');
    } else if (item.find('> .t-link').text() == "Pengaturan") {
        $('#content').load('Profile/Pengaturan');
    } else if (item.find('> .t-link').text() == "Jadwal Bimbingan") {
        $('#content').load('Bimbingan/Pemesanan');
    } else if (item.find('> .t-link').text() == "Kartu Bimbingan") {
        $('#content').load('Bimbingan/KartuBimbingan');
    } else if (item.find('> .t-link').text() == "Pengumpulan") {
        $('#content').load('Pengumpulan/PengumpulanFile');
    } else if (item.find('> .t-link').text() == "Pengumpulan Jadwal") {
        $('#content').load('Sidang/Pengumpulan_Index');
    } else if (item.find('> .t-link').text() == "Lihat Jadwal") {
        $('#content').load('Sidang/Lihat');
    } else if (item.find('> .t-link').text() == "Kalender") {
        $('#content').load('Kalender/Index');
    }
}

function onSelectMenuDosen(e) {
    var item = $(e.item);
    if (item.find('> .t-link').text() == "Pengumuman") {
        $('#content').load('PengumumanDosen/Pengumuman');
    } else if (item.find('> .t-link').text() == "Buat Pengumuman") {
        $('#content').load('PengumumanDosen/BuatPengumuman');
    } else if (item.find('> .t-link').text() == "Edit Pengumuman") {
        $('#content').load('PengumumanDosen/EditPengumuman');
    } else if (item.find('> .t-link').text() == "Biodata") {
        $('#content').load('ProfileDosen/Biodata');
    } else if (item.find('> .t-link').text() == "Sejarah") {
        $('#content').load('ProfileDosen/Sejarah');
    } else if (item.find('> .t-link').text() == "Pengaturan") {
        $('#content').load('ProfileDosen/Pengaturan');
    } else if (item.find('> .t-link').text() == "Registrasi Topik") {
        $('#content').load('Home/TopikAdmin');
    } else if (item.find('> .t-link').text() == "Registrasi Mahasiswa") {
        $('#content').load('PendaftaranDosen/RegistrasiMahasiswa');
    } else if (item.find('> .t-link').text() == "Daftar Mahasiswa") {
        $('#content').load('BimbinganDosen/Index');
    }
	else if (item.find('> .t-link').text() == "Nilai Skripsi 1") {
        $('#content').load('NilaiDosen/Index');
    }  
	else if (item.find('> .t-link').text() == "Kartu Bimbingan") {
        $('#content').load('Bimbingan/KartuBimbingan');
    } else if (item.find('> .t-link').text() == "Pengumpulan") {
        $('#content').load('Pengumpulan/PengumpulanFile');
    } else if (item.find('> .t-link').text() == "Jadwal Bimbingan") {
        $('#content').load('BimbinganDosen/Pemesanan');
    } else if (item.find('> .t-link').text() == "Pengumpulan Jadwal") {
        $('#content').load('SidangDosen/Pengumpulan_Index');
    } else if (item.find('> .t-link').text() == "Lihat Jadwal") {
        $('#content').load('Sidang/Lihat');
    } else if (item.find('> .t-link').text() == "Penilaian") {
        $('#content').load('SidangDosen/Penilaian');
    } else if (item.find('> .t-link').text() == "Kalender") {
        $('#content').load('Kalender/Index');
    }
}

function onSelectMenuKoordinator(e) {
    var item = $(e.item);
    if (item.find('> .t-link').text() == "Pengumuman") {
        $('#content').load('PengumumanKoordinator/Pengumuman');
    } else if (item.find('> .t-link').text() == "Buat Pengumuman") {
        $('#content').load('PengumumanKoordinator/BuatPengumuman');
    } else if (item.find('> .t-link').text() == "Edit Pengumuman") {
        $('#content').load('PengumumanKoordinator/EditPengumuman');
    } else if (item.find('> .t-link').text() == "File Sharing") {//penambahan baru
        $('#content').load('PengaturanFile/Index');
    }else if (item.find('> .t-link').text() == "Pengaturan Semester") {
        $('#content').load('Semester/JadwalSemester');
    } else if (item.find('> .t-link').text() == "Daftar Topik") {
        $('#content').load('Home/TopikAdmin');
    } else if (item.find('> .t-link').text() == "Daftar Mahasiswa") {
        $('#content').load('User/DaftarMahasiswa');
    } else if (item.find('> .t-link').text() == "Daftar Dosen") {
        $('#content').load('User/DaftarDosen');
    } else if (item.find('> .t-link').text() == "Daftar Akun") {
        $('#content').load('Account/DaftarAkun');
    } else if (item.find('> .t-link').text() == "Ubah Password") {
        $('#content').load('ProfileDosen/Pengaturan');
    } else if (item.find('> .t-link').text() == "Bimbingan") {
        $('#content').load('BimbinganKoordinator/Index');
    } else if (item.find('> .t-link').text() == "Pengumpulan") {
        $('#content').load('PengumpulanKoordinator/Index');
    } else if (item.find('> .t-link').text() == "Nilai") {
        $('#content').load('Nilai/Index');
    } else if (item.find('> .t-link').text() == "Pengaturan Jadwal") {
        $('#content').load('SidangKoordinator/Jadwal_Navigator');
    } else if (item.find('> .t-link').text() == "Lihat Jadwal") {
        $('#content').load('SidangKoordinator/Jadwal_Lihat');
    } else if (item.find('> .t-link').text() == "Pengaturan Sidang") {
        $('#content').load('SidangKoordinator/Sidang_Index');
    } else if (item.find('> .t-link').text() == "Kalender") {
        $('#content').load('Kalender/Index');
    }

}