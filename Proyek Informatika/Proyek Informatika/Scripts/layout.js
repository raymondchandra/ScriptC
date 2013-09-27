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
        $('#content').load('Bimbingan/PemesananIndex');
    } else if (item.find('> .t-link').text() == "Kartu Bimbingan") {
        $('#content').load('Bimbingan/KartuBimbingan');
    } else if (item.find('> .t-link').text() == "Pengumpulan") {
        $('#content').load('Pengumpulan/PengumpulanFile');
    } else if (item.find('> .t-link').text() == "Pengumpulan Jadwal") {
        $('#content').load('Sidang/Pengumpulan');
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
    } else if (item.find('> .t-link').text() == "Daftar Topik") {
        $('#content').load('Home/TopikAdmin');
    } else if (item.find('> .t-link').text() == "Registrasi Mahasiswa") {
        $('#content').load('User/Registrasi');
    } else if (item.find('> .t-link').text() == "Daftar Mahasiswa") {
        $('#content').load('User/ListMahasiswa');
    } else if (item.find('> .t-link').text() == "Kartu Bimbingan") {
        $('#content').load('Bimbingan/KartuBimbingan');
    } else if (item.find('> .t-link').text() == "Pengumpulan") {
        $('#content').load('Pengumpulan/PengumpulanFile');
    } else if (item.find('> .t-link').text() == "Jadwal Bimbingan") {
        $('#content').load('Bimbingan/PemesananIndex');
    } else if (item.find('> .t-link').text() == "Pengumpulan Jadwal") {
        $('#content').load('Sidang/Pengumpulan');
    } else if (item.find('> .t-link').text() == "Lihat Jadwal") {
        $('#content').load('Sidang/Lihat');
    } else if (item.find('> .t-link').text() == "Penilaian") {
        $('#content').load('SidangDosen/Penilaian');
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
    } else if (item.find('> .t-link').text() == "Pengaturan Jadwal Semester") {
        $('#content').load('Skripsi/PengaturanSemester');
    } else if (item.find('> .t-link').text() == "Daftar Mahasiswa") {
        $('#content').load('User/ListMahasiswa');
    } else if (item.find('> .t-link').text() == "Daftar Dosen") {
        $('#content').load('User/ListDosen');
    } else if (item.find('> .t-link').text() == "Daftar Akun") {
        $('#content').load('Account/ListAkun');
    } else if (item.find('> .t-link').text() == "Bimbingan") {
        $('#content').load('BimbinganKoordinator/Bimbingan');
    } else if (item.find('> .t-link').text() == "Pengumpulan") {
        $('#content').load('PengumpulanKoordinator/PengumpulanFile');
    } else if (item.find('> .t-link').text() == "Nilai") {
        $('#content').load('Nilai/NilaiReport');
    } else if (item.find('> .t-link').text() == "Pengaturan Jadwal") {
        $('#content').load('SidangKoordinator/PengaturanJadwal');
    } else if (item.find('> .t-link').text() == "Lihat Jadwal") {
        $('#content').load('Sidang/Lihat');
    } else if (item.find('> .t-link').text() == "Pengaturan Sidang") {
        $('#content').load('SidangKoordinator/PengaturanSidang');
    }

}

