#pragma once
#include <functional>
#include <QMenu>
#include <QWidget>

namespace ConnectionsTree {

void confirmAction(QWidget* parent, const QString& msg, std::function<void()> action,
                   QString title = "Confirm action");

void confirmAction(QWidget* parent, const QString& msg, std::function<void()> action,
                   std::function<void()> noCallback,
                   QString title = "Confirm action");

}
