#-------------------------------------------------
#
# Redis Desktop Manager Dependencies
#
#-------------------------------------------------

OTHER_FILES += $$PWD/../src/resources/qml/3rdparty/php-unserialize-js/phpUnserialize.js

# qredisclient
include($$PWD/qredisclient/qredisclient.pri)

# Easylogging
INCLUDEPATH += $$PWD/easyloggingpp/src
HEADERS += $$PWD/easyloggingpp/src/easylogging++.h

# Asyncfuture
include($$PWD/asyncfuture/asyncfuture.pri)

# Google breakpad
BREAKPADDIR = $$PWD/gbreakpad/src
DEPENDPATH += $$BREAKPADDIR

INCLUDEPATH += $$BREAKPADDIR/
INCLUDEPATH += $$BREAKPADDIR/src

#breakpad app need debug info inside binaries
win32-msvc* {
    QMAKE_CXXFLAGS += /MP
    QMAKE_LFLAGS_RELEASE += /MAP
    QMAKE_CFLAGS_RELEASE += /Zi
    QMAKE_LFLAGS_RELEASE += /debug /opt:ref
} else {
    QMAKE_CXXFLAGS+=-g
    QMAKE_CFLAGS_RELEASE+=-g
}

win32* {    
    win32-g++ {
        # Workaround for mingw
        QMAKE_LFLAGS_RELEASE=
    } else {
        INCLUDEPATH += $$PWD/qredisclient/3rdparty/windows/rmt_zlib.1.2.8.5/build/native/include
    }

    HEADERS += $$BREAKPADDIR/common/windows/string_utils-inl.h
    HEADERS += $$BREAKPADDIR/common/windows/guid_string.h
    HEADERS += $$BREAKPADDIR/client/windows/handler/exception_handler.h
    HEADERS += $$BREAKPADDIR/client/windows/common/ipc_protocol.h
    HEADERS += $$BREAKPADDIR/google_breakpad/common/minidump_format.h
    HEADERS += $$BREAKPADDIR/google_breakpad/common/breakpad_types.h
    HEADERS += $$BREAKPADDIR/client/windows/crash_generation/crash_generation_client.h
    HEADERS += $$BREAKPADDIR/common/scoped_ptr.h
    SOURCES += $$BREAKPADDIR/client/windows/handler/exception_handler.cc
    SOURCES += $$BREAKPADDIR/common/windows/string_utils.cc
    SOURCES += $$BREAKPADDIR/common/windows/guid_string.cc
    SOURCES += $$BREAKPADDIR/client/windows/crash_generation/crash_generation_client.cc
}

unix:macx { # OSX
    PRE_TARGETDEPS += $$PWD/../build/gbreakpad/Products/Release/Breakpad.framework
    LIBS += $$PWD/../build/gbreakpad/Products/Release/Breakpad.framework/Versions/A/Breakpad
    LIBS += /System/Library/Frameworks/CoreFoundation.framework/Versions/A/CoreFoundation
    LIBS += /System/Library/Frameworks/CoreServices.framework/Versions/A/CoreServices

    #deployment
    APP_DATA_FILES.files = $$PWD/../build/gbreakpad/Products/Release/Breakpad.framework
    APP_DATA_FILES.path = Contents/Frameworks
    QMAKE_BUNDLE_DATA += APP_DATA_FILES
}

unix:!macx { # ubuntu & debian
    QMAKE_CXXFLAGS += -std=gnu++0x #workaround for google breakpad

    defined(CLEAN_RPATH, var) { # clean default flags
        message("DEB package build")
        QMAKE_LFLAGS_RPATH=
        QMAKE_LFLAGS = -Wl,-rpath=\\\$$ORIGIN/../lib
        QMAKE_LFLAGS += -static-libgcc -static-libstdc++
    } else {
        # Note: uncomment if qtcreator fails to find QtCore dependencies
        #QMAKE_LFLAGS = -Wl,-rpath=/home/user/Qt5.9.3/5.9.3/gcc_64/lib
    }

    LIBS += $$BREAKPADDIR/client/linux/libbreakpad_client.a

    # Unix signal watcher
    defined(LINUX_SIGNALS, var) {
        DEFINES += LINUX_SIGNALS

        HEADERS += $$PWD/qt-unix-signals/sigwatch.h
        HEADERS += $$PWD/qt-unix-signals/sigwatch_p.h
        SOURCES += $$PWD/qt-unix-signals/sigwatch.cpp
        INCLUDEPATH += $$PWD/qt-unix-signals/
    }
}
